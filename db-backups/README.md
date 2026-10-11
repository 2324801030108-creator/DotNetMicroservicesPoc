## PostgreSQL restore guide (Sản phẩm #2)

Máy chấm: cài PostgreSQL >= 10, hoặc chạy Docker:

```powershell
docker build -t lab-postgres ./postgres
docker run -d --name postgres -e POSTGRES_PASSWORD=lab_pass -p 5432:5432 lab-postgres
# hoặc
docker compose -f scripts/infra.yml up -d postgres
```

Tạo DB (chạy 1 lần):

```powershell
psql -h localhost -U postgres -f db-backups/00-createdatabases.sql
# password: lab_pass (nếu dùng container: POSTGRES_PASSWORD=lab_pass)
```

Kiểm tra:

```sql
\l
-- phải thấy: lab_netmicro_payments, lab_netmicro_jobs, lab_netmicro_policy, lab_netmicro_pricing
```

Backup sau khi test (trên máy SV do Docker Desktop chưa start nên chưa dump được, thầy dump trên máy thầy):

```powershell
pg_dumpall -U postgres > db-backups/full-backup.sql
pg_dump -U lab_user -h localhost lab_netmicro_policy > db-backups/lab_netmicro_policy.sql
pg_dump -U lab_user -h localhost lab_netmicro_payments > db-backups/lab_netmicro_payments.sql
pg_dump -U lab_user -h localhost lab_netmicro_jobs > db-backups/lab_netmicro_jobs.sql
pg_dump -U lab_user -h localhost lab_netmicro_pricing > db-backups/lab_netmicro_pricing.sql
```
