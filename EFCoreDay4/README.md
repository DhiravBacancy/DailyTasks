# Entity Framework : Day - 5 
## Problem Statement:
- Create a fully functional .NET Core Web API project that demonstrates mastery over Entity Framework Core (EF Core) concepts including model creation, relationships, migrations, data seeding, CRUD operations, and data loading techniques.
- **Assignment:** https://docs.google.com/document/d/1ixE6Jgn-uYzyapkCl0Ys6dm6FN0lEINkfRsqPSde4BM/edit?tab=t.0#heading=h.uw2xpk5yae2


## 📌 API Endpoints

### **Department Controller**
| Method  | Endpoint               | Description                         |
|---------|------------------------|-------------------------------------|
| POST    | `/api/departments`      | Create a new department            |
| GET     | `/api/departments/{id}` | Get department details with employees |
| PUT     | `/api/departments/{id}` | Update department information      |
| DELETE  | `/api/departments/{id}` | Delete a department                |

### **Employee Controller**
| Method  | Endpoint                            | Description                                |
|---------|-------------------------------------|--------------------------------------------|
| POST    | `/api/employees`                   | Create a new employee                     |
| GET     | `/api/employees/{id}`              | Get employee details by ID                |
| PUT     | `/api/employees/{id}`              | Update employee information               |
| DELETE  | `/api/employees/{id}`              | Delete an employee                        |

### **Project Controller**
| Method  | Endpoint               | Description                        |
|---------|------------------------|------------------------------------|
| POST    | `/api/projects`        | Create a new project              |
| GET     | `/api/projects/{id}`   | Get project details with employees |
| PUT     | `/api/projects/{id}`   | Update project details            |
| DELETE  | `/api/projects/{id}`   | Delete a project                  |

