# BÀI THỰC HÀNH SỐ 4 – PHẦN 2: CÀI ĐẶT MÔI TRƯỜNG (.NET Microservices – Đại lý bảo hiểm)

Solution: `DotNetMicroservicesPoc.sln` — Build **succeeded, 0 errors** (SDK .NET 8).

## 1. Task #1 – Quy tắc tổ chức source code

Mỗi microservice gồm 3 projects (đúng quy tắc API definition / Implementation / Test):

| Microservice | API definition | Implementation | Test |
|---|---|---|---|
| Auth | AuthService.Api | AuthService | AuthService.Test |
| Chat | ChatService.Api | ChatService | ChatService.Test |
| Pricing | PricingService.Api | PricingService | PricingService.Test |
| Policy | PolicyService.Api | PolicyService | PolicyService.Test |
| PolicySearch | PolicySearchService.Api | PolicySearchService | PolicySearchService.Test |
| Payment | PaymentService.Api | PaymentService | PaymentService.Test |
| Product | ProductService.Api | ProductService | ProductService.Test |
| Document | DocumentService.Api | DocumentService | DocumentService.Test |
| Dashboard | DashboardService.Api | DashboardService | DashboardService.Test |

- `*.Api` (classlib): Commands / Queries / Events / DTOs = "cổng" trong kiến trúc ports & adapters.
  Ví dụ `PolicyService.Api/Commands/PolicyCommands.cs`, `ProductService.Api/Queries/Dtos/ProductDtos.cs`,
  `DashboardService.Api/Queries/DashboardQueries.cs`, `PolicySearchService.Api/Queries/*`.
- Implementation (webapi): Controllers + Handlers (MediatR `IRequestHandler`, `INotificationHandler`),
  Adapters REST/SignalR. Ví dụ `PolicyService/Handlers/*`, `PolicyService/Controllers/PolicyController.cs`.
- Test (xUnit): unit + integration. `dotnet test` đã pass 9/9.

Ngoài ra solution bao gồm sẵn:
- `AgentPortalApiGateway/` (Ocelot + Eureka + JWT, từ file nén đề bài)
- `BlazorWasmClient/` (Blazor WASM, đã fix tương thích DTO để build 0 lỗi)
- `Web/` (VueJS SPA), `eureka-server/` (Java), `postgres/` (Dockerfile + createdatabases.sql), `scripts/` (app.yml, infra.yml)

Xem `dotnet sln list` để thấy đủ 31 projects.

## 2. Task #2 – Code ban đầu

1. **MediatR 12.4.1** đã cài cho cả 9 Implementation + 9 Api:
   `dotnet add <proj> package MediatR --version 12.4.1`
   Đăng ký DI trong `Program.cs` mỗi service (tương đương `Startup.ConfigureServices` bản cũ):
   ```csharp
   builder.Services.AddControllers();
   builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
   ```
2. PostgreSQL >= 10: dùng `postgres/Dockerfile` (postgres:11.4) + `postgres/createdatabases.sql`.
   Chạy: `docker build -t lab-postgres ./postgres` hoặc `docker compose -f scripts/infra.yml up postgres`.
   File nộp: `db-backups/` (xem mục 4).
3. RabbitMQ: `scripts/infra.yml` service `rabbitmq:3-management` (5672, 15672).
4. Elasticsearch >= 6: `scripts/infra.yml` dùng image `elasticsearch:8.9.2` (9200, single-node).
5. Docker + Compose: đã kiểm tra `docker --version` (29.8.2). File `scripts/app.yml` build 9 services + gateway + web-vue.
6-11. Đã copy nguyên 6 thư mục đề cho vào solution: `eureka-server`, `postgres`, `scripts`, `Web`, `BlazorWasmClient`, `AgentPortalApiGateway`.

Build toàn hệ thống:
```
dotnet build DotNetMicroservicesPoc.sln --nologo
```
Kết quả: **Build succeeded, 0 Error(s)** — xem `build-result.txt`.
Chỉ còn warnings (không tính lỗi): NU1903/NU1901 của `Steeltoe.Discovery.Eureka 3.2.7` trong Gateway (có sẵn từ đề) + vài CS8632/CS8669/CA2017 trong code mẫu Blazor.

## 3. Sản phẩm #1 – GitHub

```powershell
cd C:\BTKiemThu\DotNetMicroservicesPoc
git init; git add .; git commit -m "Lab4 P2: microservices skeleton + MediatR + gateway/blazor/web build OK"
gh repo create DotNetMicroservicesPoc --private --source=. --remote=origin --push
# Settings > Collaborators > Add people: caotxuan@gmail.com (Role: Collaborator/Reporter)
```
Nếu chưa tạo được repo GitHub thì nén thư mục này (xóa `bin/`, `obj/` trước) đưa lên Drive nộp link eLearning (tối đa 8đ).

## 4. Sản phẩm #2 – Backup PostgreSQL

`postgres/createdatabases.sql` tạo user + 4 DB:
- `lab_netmicro_payments`, `lab_netmicro_jobs`, `lab_netmicro_policy`, `lab_netmicro_pricing` (owner `lab_user`/`lab_pass`).

Nộp trong `db-backups/`:
- `00-createdatabases.sql` (bản gốc để thầy chạy `psql -U postgres -f 00-createdatabases.sql`)
- Do máy làm bài chưa khởi động được Docker Desktop / chưa cài PostgreSQL server nên chưa `pg_dump` được file binary;
  thầy restore bằng cách chạy script trên, hoặc sau khi `docker compose up postgres` thì dump:
```
docker exec postgres pg_dumpall -U postgres > full-backup.sql
docker exec postgres pg_dump -U lab_user lab_netmicro_policy > lab_netmicro_policy.sql
```

## 5. Sản phẩm #3 – Ảnh build

File `build-result.txt` là log `dotnet build` (0 errors).
Chụp màn hình Visual Studio / terminal khi build và nộp kèm ảnh. Càng ít lỗi càng cao điểm — hiện tại **0 lỗi**.

Hạn nộp: trước 18h00 ngày 11/10/2026.

## 6. Bài thực hành số 7 – Lưu trữ & truy cập dữ liệu với Marten

### Task #1 – Thêm Marten vào dự án
- Đã cài `Marten 7.19.1` (NuGet) cho `PaymentService`:
  ```
  dotnet add PaymentService package Marten --version 7.19.1
  ```
- Cấu hình trong `PaymentService/DataAccess/Marten/MartenInstaller.cs`:
  ```csharp
  services.AddMarten(Configuration.GetConnectionString("PgConnection"));
  ```
  Chuỗi kết nối `PgConnection` nằm trong `PaymentService/appsettings.json`
  (`lab_netmicro_payments`, user `lab_user`/`lab_pass`).
- Kèm theo các gói phục vụ code mẫu: `Hangfire.AspNetCore`, `Hangfire.PostgreSql`,
  `EasyNetQ`, `GlobalExceptionHandler`, `Microsoft.AspNetCore.Mvc.NewtonsoftJson`.

### Task #2 – Bổ sung code PaymentService (3 thư mục)
- `PaymentService/` (Implementation): `Domain/`, `DataAccess/` (Marten document store),
  `Messaging/` (RabbitMQ listeners), `Jobs/` (Hangfire), `Init/`, `Queries/`,
  `Listeners/`, `Extensions/`, `Infrastructure/`, `Controllers/PaymentController.cs`,
  `Startup.cs`.
- `PaymentService.Api/`: `Commands/`, `Queries/`, `Events/`, `Exceptions/`.
- `PaymentService.Test/`: `Domain/PolicyAccountTest.cs`, `MockPolicyAccountRepository.cs`.
- Đã bổ sung `PolicyCreated`/`PolicyTerminated` trong `PolicyService.Api/Events/PolicyEvents.cs`
  để các listener của PaymentService, ChatService và DashboardService cùng biên dịch.

### Task #3 – Bổ sung code ProductService (3 thư mục)
- `ProductService/` (Implementation): `Domain/`, `DataAccess/` (EF Core InMemory),
  `Commands/`, `Queries/`, `Init/`, `Controllers/ProductsController.cs`, `Program.cs`.
- `ProductService.Api/`: `Commands/` (+ DTOs), `Queries/` (+ DTOs, Converters).
- `ProductService.Test/`: `Domain/`, `Handlers/`, `Controllers/`, `TestData/`.
- Đã xoá file DTO trùng lặp `ProductService.Api/Queries/Dtos/ProductDtos.cs` và thay bằng
  các file DTO tách riêng theo code mẫu (`ProductDto.cs`, `ChoiceDto.cs`, `CoverDto.cs`,
  `QuestionDto.cs`).

### Kết quả kiểm tra
```
dotnet build DotNetMicroservicesPoc.sln --nologo
    30 Warning(s)
    0 Error(s)
```
```
dotnet test PaymentService.Test      -> Passed: 6,  Failed: 0
dotnet test ProductService.Test      -> Passed: 14, Failed: 0
```
Log build đầy đủ: `build-result.txt`. Link GitHub nộp bài: `TestResult_linkgithub.txt`
(https://github.com/2324801030108-creator/DotNetMicroservicesPoc).
