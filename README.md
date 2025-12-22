
# WhalePlatform

**WhalePlatform** is an **e-commerce platform** built using a **microservices architecture**, focusing on scalability, modularity, and reusability.

## 📁 Project Structure

The solution is organized into the following main areas:

### 🔧 BuildingBlocks
Reusable components shared across services.

- **Core**  
  Shared core logic and common abstractions used by multiple projects.

### 🧩 Services
- Independent **Web APIs** that implement the platform’s business capabilities.

### 🌐 Web
- The **web application** (frontend) that consumes the backend services.


## 🐇 RabbitMQ

RabbitMQ is used for asynchronous communication between services.

### ▶️ Running RabbitMQ locally

Use the following command to start RabbitMQ with the management UI:

```bash
docker run -d \
  --hostname rabbit-host \
  --name rabbit-whaleplatform \
  -p 15672:15672 \
  -p 5672:5672 \
  rabbitmq:management