using HotelsBookings.Server.DataModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseInMemoryDatabase("MyTestDb"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins("https://localhost:5173")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
});


builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
app.UseDefaultFiles();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDBContext>();
    AddDummyData(context);
}

app.Run();

static void AddDummyData(AppDBContext context)
{
    if (!context.Hotels.Any())
    {
        var dummyHotels = new List<Hotel>
        {
            new() {
                HotelName = "Hotel Paradise",
                Location = "Turkey, Belek",
                Image = "./src/assets/Images/pexels-thorsten-technoman-109353-338504.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Ocean View",
                Location = "Philippines, El Nido",
                Image = "./src/assets/Images/pexels-boonkong-boonpeng-442952-1134176.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Mountain Retreat",
                Location = "Turkey, Antalya",
                Image = "./src/assets/Images/pexels-asman-chema-91897-594077.jpg",
                Bookings = []
            },
            new() {
                HotelName = "City Lights Hotel",
                Location = "Greece, Heraklion",
                Image = "./src/assets/Images/pexels-asadphoto-1268871.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Desert Oasis",
                Location = "Philippines, El Nido",
                Image = "./src/assets/Images/pexels-michael-block-1691617-3225531.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Lakeside Inn",
                Location = "Maldives",
                Image = "./src/assets/Images/pexels-ishan-139144-678725.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Sunset Resort",
                Location = "Maldives",
                Image = "./src/assets/Images/pexels-asadphoto-1287460.jpg",
                Bookings = []
            },
            new() {
                HotelName = "Coastal Escape",
                Location = "Egypt, Hurgada",
                Image = "./src/assets/Images/pexels-pixabay-261429.jpg",
                Bookings = []
            }
        };

        context.Hotels.AddRange(dummyHotels);
        context.SaveChanges();
    }
}
