## DbContextの生成
- 次のコマンドを実行して、DbContextを生成します

```bash
dotnet ef dbcontext scaffold -f "Data Source=localhost\sqlexpress;Integrated Security=True;Initial Catalog=UsedCarDb" Microsoft.EntityFrameworkCore.SqlServer --context-dir . --output-dir .\Models --context UsedCarDBContext --project .\src\UsedCar.Backend.Infrastructures.EntityFrameworkCore\UsedCar.Backend.Infrastructures.EntityFrameworkCore.csproj
```
