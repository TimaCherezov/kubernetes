Kubernetes

- ФИО: Тимофей Черёзов
- Группа: ФТ-304
- ОС: Ubuntu 24.04 в WSL2
- Архитектура: amd64
2. Приложение 
HTTP-приложение возвращает JSON с сообщением, автором и признаком наличия токена. Порт - 8080.
- APP_MESSAGE, APP_ENV - из ConfigMap.
- APP_TOKEN - фиктивный токен из Secret; приложение проверяет его наличие.
3. Сборка и запуск

docker build -t localhost:5001/kuber-homework:v1 .
docker push localhost:5001/kuber-homework:v1

kubectl apply -f k8s/namespace.yaml
kubectl apply -f k8s/configmap.yaml
kubectl apply -f k8s/secret.yaml
kubectl apply -f k8s/deployment.yaml
kubectl -n homework-1 rollout status deployment/kuber-homework

kubectl -n homework-1 port-forward deployment/kuber-homework 8082:8080
В другом терминале: curl -i http://localhost:8082/

4. 
- Образ: localhost:5001/kuber-homework:v1
- Успешный push (digest): sha256:b580ee8773f059740841d2273f3d8892e140c8d4bbe2152f17099dbbe24bbcc2 size: 856
5.
deployments,replicasets,pods.txt - список Deployment, ReplicaSet и Pod.
http.txt - HTTP-ответ приложения.
pods.txt - список Pod до и после удаления.
change_message.txt - результаты изменения ConfigMap.

Эксперимент 1. Удаление Pod

ReplicaSet создал новый Pod, чтобы восстановить заданные Deployment две реплики. 
Это новый Pod, а не перезапуск контейнера: у него другое имя и новый UID. 
При перезапуске контейнера UID Pod остаётся прежним.

Эксперимент 2. Изменение ConfigMap

После изменения ConfigMap уже запущенное приложение продолжало возвращать старое значение APP_MESSAGE, 
потому что переменные окружения считываются при запуске контейнера и автоматически не обновляются.
После выполнения kubectl rollout restart deployment/kuber-homework Deployment пересоздал Pod'ы. 
Новые контейнеры получили обновлённое значение APP_MESSAGE из ConfigMap, поэтому приложение стало возвращать новое сообщение. 
Пересобирать образ не потребовалось, поскольку код приложения не менялся.
6. 
- Labels и selector: labels - метки объектов; selector выбирает объекты по меткам. Deployment использует его для управления своими Pod.
- Requests и limits: requests - сколько ресурсов Kubernetes должен зарезервировать для контейнера при размещении на узле; limits ограничивают потребление ресурса контейнером.
- ConfigMap и Secret: ConfigMap хранит обычную конфигурацию, Secret - чувствительные данные.
- Base64: это кодирование, а не шифрование; данные можно декодировать без ключа.
7. Очистка
   kubectl delete -f k8s/namespace.yaml