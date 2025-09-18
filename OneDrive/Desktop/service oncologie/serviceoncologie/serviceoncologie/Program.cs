using Microsoft.EntityFrameworkCore;
using serviceoncologie.Data;
using serviceoncologie.Repositories;
using serviceoncologie.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configurer le DbContext pour utiliser MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("myCon"),
        new MySqlServerVersion(new Version(5, 7, 36)))); // Utiliser la version 5.7.36 de MySQL

// Ajouter les repositories pour l'injection de dépendances
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITacheRepository, TacheRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IRdvRepository, RdvRepository>();
builder.Services.AddScoped<IPaiementRepository, PaiementRepository>();
builder.Services.AddScoped<IMaladieRepository, MaladieRepository>();
builder.Services.AddScoped<IConsultationMaladieRepository, ConsultationMaladieRepository>();
builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();
builder.Services.AddScoped<IStafMedecinRepository, StafMedecinRepository>();
builder.Services.AddScoped<ICommissionStafRepository, CommissionStafRepository>();
builder.Services.AddScoped<IAdmissionRepository, AdmissionRepository>();
builder.Services.AddScoped<IDecisionStafRepository, DecisionStafRepository>();
builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
builder.Services.AddScoped<ICureRepository, CureRepository>();
builder.Services.AddScoped<IProtocoleRepository, ProtocoleRepository>();
builder.Services.AddScoped<IDciRepository, DciRepository>();
builder.Services.AddScoped<IDciMedicamentRepository, DciMedicamentRepository>();
builder.Services.AddScoped<IDossierRepository, DossierRepository>();
builder.Services.AddScoped<IPrestationMedicaleRepository, PrestationMedicaleRepository>();
builder.Services.AddScoped<IConsultationPrestationRepository, ConsultationPrestationRepository>();
builder.Services.AddScoped<IStatistiquesRepository, StatistiquesRepository>();










// Ajouter la configuration pour CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.WithOrigins("http://localhost:4200") // Port de ton front-end
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Ajouter les services de contrôleurs et la prise en charge de JSON
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Ajouter la configuration pour Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ajouter la redirection HTTPS si nécessaire
app.UseHttpsRedirection();

// Utiliser la politique CORS
app.UseCors("AllowAll");

// Ajouter l'autorisation (si nécessaire pour ton application)
app.UseAuthorization();

// Mapper les contrôleurs
app.MapControllers();

app.Run();
