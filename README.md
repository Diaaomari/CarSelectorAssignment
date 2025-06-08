# Car Selector Assignment

A .NET 8 solution for selecting car makes, years, types, and models using the NHTSA API. It includes a Web UI and an API layer, fully containerized with Docker.

---

## 📁 Project Structure

- `CarSelector.Web`: ASP.NET Core MVC Web App
- `CarSelector.API`: ASP.NET Core Web API
- `CarSelector.Application`: DTOs and Logic Layer
- `CarSelector.Infrastructure`: Integration Layer
- `docker-compose.yml`: Container orchestration config

---

## 🚀 Getting Started with Docker Compose

### 1. Clone the Repository

```bash
git clone https://github.com/Diaaomari/CarSelectorAssignment
cd CarSelectorAssignment
```

---

### 2. Run the Application

```bash
docker-compose up --build
```

This command builds and runs both the API and Web containers.

---

### 3. Access the Web App

Open your browser at:

```
http://localhost:5080
```

If you want to test the API directly:

```
http://localhost:5077/swagger
```

---

## 🔧 Environment Variables (auto-handled in Compose)

| Project           | Variable Name     | Value                                           |
|------------------|-------------------|-------------------------------------------------|
| API               | `NhtsaBaseUrl`    | `https://vpic.nhtsa.dot.gov/api/vehicles/`     |
| Web               | `CarApiBaseUrl`   | `http://api:8080/api/Vehicles/`                |

---

## 📦 Exposed Ports

| Service | Port in Container | Port on Host |
|---------|-------------------|--------------|
| API     | 8080              | 5077         |
| Web     | 8080              | 5080         |

---

## 🛑 Stop and Remove Containers

```bash
docker-compose down
```

To rebuild everything from scratch:

```bash
docker-compose down --volumes --remove-orphans
docker-compose up --build
```

---

## 🧪 Troubleshooting

- If the site doesn't load, try: `docker-compose logs web` or `docker-compose logs api`
- Make sure Docker Desktop is running.
- Port conflicts? Make sure nothing else is using ports 5077 or 5080.

---

## 📬 Contact

📬 Need Help?
For any questions or support regarding this project, feel free to contact me at diaa.omari12@gmail.com.
---
