using Microsoft.EntityFrameworkCore;
using TTV.Domain.Entities;
using TTV.Infrastructure.DataAccess;

namespace TTV.DatabaseDeploy
{
    internal class SampleData
    {
        private readonly DataContext dataContext;

        public SampleData(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<SampleData> PopulateAsync()
        {
            await using var transaction = await dataContext.Database.BeginTransactionAsync();
            try
            {
                Console.WriteLine("Running migrations.");
                await dataContext.Database.MigrateAsync();

                Console.WriteLine("Syncing Data.");
                await SetIdentityInsertAsync<Lesson>(true);
                SyncLessons();

                Console.WriteLine("Saving changes.");
                await dataContext.SaveChangesAsync();
                await SetIdentityInsertAsync<Lesson>(false);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return this;
        }

        private async Task SetIdentityInsertAsync<TEntity>(bool enabled) where TEntity : BaseEntity
        {
            var entityType = dataContext.Model.FindEntityType(typeof(TEntity)) ?? throw new InvalidOperationException($"Entity type {typeof(TEntity)} not found in model.");
            var sql = $"SET IDENTITY_INSERT [{entityType.GetSchema()}].[{entityType.GetTableName()}] {(enabled ? "ON" : "OFF")}";
            await dataContext.Database.ExecuteSqlRawAsync(sql);
        }

        public void SyncLessons()
        {
            Console.WriteLine("\tSyncing Lessons.");
            var source = new List<Lesson>()
            {
                new Lesson() { Id = 1, Title = "Finding the Resultant of Multiple Forces", LessonType = LessonType.Video },
                new Lesson() { Id = 2, Title = "Components of Angled Forces", LessonType = LessonType.Video },
                new Lesson() { Id = 3, Title = "Forces in Equilibrium", LessonType = LessonType.Video },
                new Lesson() { Id = 4, Title = "Forces on an Inclined Surface", LessonType = LessonType.Video },
                new Lesson() { Id = 5, Title = "The Force of Normal", LessonType = LessonType.Video },
                new Lesson() { Id = 6, Title = "Frictional Forces", LessonType = LessonType.Video },
                new Lesson() { Id = 7, Title = "Coefficients of Friction", LessonType = LessonType.Video },
                new Lesson() { Id = 8, Title = "Newton's First Law of Motion", LessonType = LessonType.Video },
                new Lesson() { Id = 9, Title = "Newton's Second Law of Motion", LessonType = LessonType.Video },
                new Lesson() { Id = 10, Title = "Newton's 2nd Law Questions Involving Simultaneous Equations", LessonType = LessonType.Video },
                new Lesson() { Id = 11, Title = "Newton's Third Law of Motion", LessonType = LessonType.Video },
                new Lesson() { Id = 12, Title = "Newton's Law of Universal Gravitation", LessonType = LessonType.Video },
                new Lesson() { Id = 13, Title = "Electrostatics Part 1", Description = "Revision of basics, Coulomb's Law (theory and calculations.)", LessonType = LessonType.Video },
                new Lesson() { Id = 14, Title = "Electrostatics Part 2", Description = "Electric fields, electric field strength at a point (theory and calculations), other electrostatics calculations.", LessonType = LessonType.Video },
                new Lesson() { Id = 15, Title = "Electric Circuits Part 1", Description = "Potential difference, current, resistance, EMF, ammeters, voltmeters, Ohm’s Law, Ohmic vs. non-Ohmic conductors.",LessonType = LessonType.Video },
                new Lesson() { Id = 16, Title = "Electric Circuits Part 2", Description = "Series circuits, parallel circuits and combination circuits.", LessonType = LessonType.Video },
                new Lesson() { Id = 17, Title = "Electric Circuits Part 3", Description = "The effect of adding/removing resistors in series/parallel, electrical power, cost of electricity using kilowatthours, electric circuits calculations.", LessonType = LessonType.Video },
                new Lesson() { Id = 18, Title = "Converting Moles Between Different Substances in a Chemical Reaction", LessonType = LessonType.Video },
                new Lesson() { Id = 19, Title = "Limiting Reagents", LessonType = LessonType.Video },
                new Lesson() { Id = 20, Title = "Percentage Purity", LessonType = LessonType.Video },
                new Lesson() { Id = 21, Title = "Percentage Yield", LessonType = LessonType.Video },
                new Lesson() { Id = 22, Title = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules", LessonType = LessonType.Video },
                new Lesson() { Id = 23, Title = "Intermolecular Forces", LessonType = LessonType.Video },
                new Lesson() { Id = 24, Title = "Determining Molecular Shape using the VSEPR Theory", LessonType = LessonType.Video },
                new Lesson() { Id = 25, Title = "Energy and Chemical Change", LessonType = LessonType.Video },
            };

            var existingLessons = dataContext.Lessons.ToList();

            foreach (var sourceLesson in source)
            {
                var existingLesson = existingLessons.FirstOrDefault(l => l.Id == sourceLesson.Id);

                if (existingLesson == null)
                {
                    Console.WriteLine($"\t\tAdding lesson ({sourceLesson.Id}) {sourceLesson.Title}.");
                    dataContext.Lessons.Add(sourceLesson);
                }
                else
                {
                    Console.WriteLine($"\t\tUpdating lesson ({sourceLesson.Id}) {sourceLesson.Title}.");
                    dataContext.Entry(existingLesson).CurrentValues.SetValues(sourceLesson);
                }
            }

            foreach (var existingLesson in existingLessons)
            {
                if (!source.Any(l => l.Id == existingLesson.Id))
                {
                    Console.WriteLine($"\t\tRemoving lesson ({existingLesson.Id}) {existingLesson.Title}.");
                    dataContext.Lessons.Remove(existingLesson);
                }
            }
        }
    }
}
