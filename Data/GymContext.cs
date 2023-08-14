using GymApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GymApi.Data;

public class GymContext: DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseDetail> ExercisesDetail { get; set; }
    public DbSet<Workout> Workouts { get; set; }

    public GymContext(DbContextOptions<GymContext> options): base(options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workout>()
            .HasOne(w => w.Client) // Relação Um-para-Muitos (um treino pertence a um cliente)
            .WithMany(c => c.Workouts) // Um cliente pode ter vários treinos
            .HasForeignKey(w => w.ClientId); // Chave estrangeira para o Cliente
    }
}
