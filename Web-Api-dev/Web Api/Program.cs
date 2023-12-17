using Infrastructure;
using Application.Contracts;
using Application.Services;
using Context;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web_Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(origin => origin == "https://member5-8.smarterasp.net/cp/filemanager.asp?d=h:"); ;
    });
});

builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("SecretKey").Value);
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.SaveToken = true;
    TokenValidationParameters token = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
}).AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins();
    });
});

builder.Services.AddScoped<IProductReposatory, ProductReposatory>();
builder.Services.AddScoped<IProductServices, ProductSrvices>();
builder.Services.AddScoped<ICategoryReposatory, CategoryReposatory>();
builder.Services.AddScoped<IcategoryServices, CategoryService>();
builder.Services.AddScoped<ISubCategoryReposatory, SubCategoryReposatory>();
builder.Services.AddScoped<ISubcategoryServices, SubCategoryService>();
builder.Services.AddScoped<IOrderItemReposatory, OrderItemReposatory>();
builder.Services.AddScoped<IOrderItemService, OrderItemServices>();
builder.Services.AddScoped<IImageReposatory, ImageReposatory>();
builder.Services.AddScoped<IOrderReposatory, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IcountryReposatory, CountryReposatory>();
builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICityReposatory, CityReposatory>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressReposatory>();
builder.Services.AddScoped<IShippingAddressServices, ShippingAddressServices>();
builder.Services.AddScoped<IUserReposatory, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHub<OrderHub>("/orderHub");
app.Run();