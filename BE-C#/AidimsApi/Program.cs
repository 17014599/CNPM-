using AidimsApi.Data;
using AidimsApi.Models; // 👈 Dùng cho thêm user mẫu
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 👉 Cấu hình CORS (chỉ cho phép frontend truy cập)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500") // Live Server (VSCode)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 👉 Cấu hình DbContext dùng SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 👉 Cấu hình HTTP Client (để gọi Flask AI Service)
builder.Services.AddHttpClient();

// 👉 Thêm dịch vụ controller
builder.Services.AddControllers();

var app = builder.Build();

// 👉 Áp dụng CORS
app.UseCors();

// 👉 HTTPS + Authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// 👉 Map route controller
app.MapControllers();

// ✅ THÊM NGƯỜI DÙNG MẪU (chỉ khi database đang trống)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Username = "admin1", Password = "admin123", Role = "admin" },
            new User { Username = "doc1", Password = "doc123", Role = "physician" },
            new User { Username = "reception1", Password = "re123", Role = "reception" },
            new User { Username = "tech1", Password = "tech123", Role = "technician" },
            new User { Username = "ai1", Password = "ai123", Role = "ai_handler" }
        );
        db.SaveChanges();
    }
}

app.Run();
