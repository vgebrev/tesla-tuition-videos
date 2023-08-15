using Microsoft.EntityFrameworkCore;
using TTV.Domain.Entities;
using TTV.Infrastructure.DataAccess;

namespace TTV.DatabaseDeploy
{
    internal class SampleData
    {
        private readonly DataContext dataContext;
        private static TagCategory[] TagCategories { get; } = new TagCategory[4]
        {
            new TagCategory() { Id = 1, Name = "Paper", Priority = 0 },
            new TagCategory() { Id = 2, Name = "Topic", Priority = 1 },
            new TagCategory() { Id = 3, Name = "Syllabus", Priority = 2 },
            new TagCategory() { Id = 4, Name = "Other", Priority = 3}
        };

        private static Tag[] Tags { get; } = new Tag[19]
        {
            // Paper
            new Tag() { Id = 1, Name = "Physics", Category = TagCategories[0] },
            new Tag() { Id = 2, Name = "Chemistry", Category = TagCategories[0] },
            
            //Topic
            new Tag() { Id = 3, Name = "Mechanics", Category = TagCategories[1] },
            new Tag() { Id = 4, Name = "Kinematics", Category = TagCategories[1] },
            new Tag() { Id = 5, Name = "Electrostatics", Category = TagCategories[1] },
            new Tag() { Id = 6, Name = "Electric Circuits", Category = TagCategories[1] },
            new Tag() { Id = 7, Name = "Stoichiometry", Category = TagCategories[1] },
            new Tag() { Id = 8, Name = "Quantitative Aspects of Chemical Change", Category = TagCategories[1] },
            new Tag() { Id = 9, Name = "Chemical Bonding", Category = TagCategories[1] },
            new Tag() { Id = 10, Name = "Atomic Combinations", Category = TagCategories[1] },
            new Tag() { Id = 11, Name = "Intermolecular Forces", Category = TagCategories[1] },
            
            // Syllabus
            new Tag() { Id = 12, Name = "IEB", Category = TagCategories[2] },
            new Tag() { Id = 13, Name = "GDE", Category = TagCategories[2] },

            // Other
            new Tag() { Id = 14, Name = "Grade 10", Category = TagCategories[3] },
            new Tag() { Id = 15, Name = "Grade 11", Category = TagCategories[3] },
            new Tag() { Id = 16, Name = "Grade 12", Category = TagCategories[3] },
            new Tag() { Id = 17, Name = "Vectors", Category = TagCategories[3] },
            new Tag() { Id = 18, Name = "Forces", Category = TagCategories[3] },
            new Tag() { Id = 19, Name = "Newton's Laws", Category = TagCategories[3] },
        };


        private static Video[] IntroVideos { get;  } = new Video[25]
        {
            new Video() { Id = 1, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 2, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 3, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 4, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 5, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 6, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 7, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 8, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 9, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 10, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 11, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 12, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 13, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 14, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 15, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 16, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 17, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 18, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 19, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 20, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 21, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 22, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 23, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 24, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
            new Video() { Id = 25, Filename = "TTV-Intro-Placeholder.mp4", VideoType = VideoType.Intro },
        };

        private static Video[] LessonVideos { get; } = new Video[25]
        {
            new Video() { Id = 26, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[0] },
            new Video() { Id = 27, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[1] },
            new Video() { Id = 28, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[2] },
            new Video() { Id = 29, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[3] },
            new Video() { Id = 30, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[4] },
            new Video() { Id = 31, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[5] },
            new Video() { Id = 32, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[6] },
            new Video() { Id = 33, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[7] },
            new Video() { Id = 34, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[8] },
            new Video() { Id = 35, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[9] },
            new Video() { Id = 36, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[10] },
            new Video() { Id = 37, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[11] },
            new Video() { Id = 38, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[12] },
            new Video() { Id = 39, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[13] },
            new Video() { Id = 40, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[14] },
            new Video() { Id = 41, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[15] },
            new Video() { Id = 42, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[16] },
            new Video() { Id = 43, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[17] },
            new Video() { Id = 44, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[18] },
            new Video() { Id = 45, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[19] },
            new Video() { Id = 46, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[20] },
            new Video() { Id = 47, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[21] },
            new Video() { Id = 48, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[22] },
            new Video() { Id = 49, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[23] },
            new Video() { Id = 50, Filename = "TTV-Lesson-Placeholder.mp4", VideoType = VideoType.FullLesson, Intro = IntroVideos[24] }
        };

        private static Lesson[] Lessons { get; } = new Lesson[25]
        {
            // Physics
            new Lesson() { Id = 1, Title = "Finding the Resultant of Multiple Forces", LessonType = LessonType.Video,
                Video = LessonVideos[0],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 2, Title = "Components of Angled Forces", LessonType = LessonType.Video,
                Video = LessonVideos[1],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 3, Title = "Forces in Equilibrium", LessonType = LessonType.Video,
                Video = LessonVideos[2],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 4, Title = "Forces on an Inclined Surface", LessonType = LessonType.Video,
                Video = LessonVideos[3],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 5, Title = "The Force of Normal", LessonType = LessonType.Video,
                Video = LessonVideos[4],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 6, Title = "Frictional Forces", LessonType = LessonType.Video,
                Video = LessonVideos[5],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 7, Title = "Coefficients of Friction", LessonType = LessonType.Video,
                Video = LessonVideos[6],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 8, Title = "Newton's First Law of Motion", LessonType = LessonType.Video,
                Video = LessonVideos[7],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 9, Title = "Newton's Second Law of Motion", LessonType = LessonType.Video,
                Video = LessonVideos[8],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 10, Title = "Newton's 2nd Law Questions Involving Simultaneous Equations", LessonType = LessonType.Video,
                Video = LessonVideos[9],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 11, Title = "Newton's Third Law of Motion", LessonType = LessonType.Video,
                Video = LessonVideos[10],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 12, Title = "Newton's Law of Universal Gravitation", LessonType = LessonType.Video,
                Video = LessonVideos[11],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18] } },
            new Lesson() { Id = 13, Title = "Electrostatics Part 1", Description = "Revision of basics, Coulomb's Law (theory and calculations.)", LessonType = LessonType.Video,
                Video = LessonVideos[12],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[4], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 14, Title = "Electrostatics Part 2", Description = "Electric fields, electric field strength at a point (theory and calculations), other electrostatics calculations.", LessonType = LessonType.Video,
                Video = LessonVideos[13],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[4], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 15, Title = "Electric Circuits Part 1", Description = "Potential difference, current, resistance, EMF, ammeters, voltmeters, Ohm’s Law, Ohmic vs. non-Ohmic conductors.",LessonType = LessonType.Video,
                Video = LessonVideos[14],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[5], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 16, Title = "Electric Circuits Part 2", Description = "Series circuits, parallel circuits and combination circuits.", LessonType = LessonType.Video,
                Video = LessonVideos[15],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[5], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 17, Title = "Electric Circuits Part 3", Description = "The effect of adding/removing resistors in series/parallel, electrical power, cost of electricity using kilowatthours, electric circuits calculations.", LessonType = LessonType.Video,
                Video = LessonVideos[16],
                Tags = new HashSet<Tag>(){ Tags[0], Tags[5], Tags[11], Tags[12], Tags[14] } },
            
            // Chemistry
            new Lesson() { Id = 18, Title = "Converting Moles Between Different Substances in a Chemical Reaction", LessonType = LessonType.Video,
                Video = LessonVideos[17],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 19, Title = "Limiting Reagents", LessonType = LessonType.Video,
                Video = LessonVideos[18],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 20, Title = "Percentage Purity", LessonType = LessonType.Video,
                Video = LessonVideos[19],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 21, Title = "Percentage Yield", LessonType = LessonType.Video,
                Video = LessonVideos[20],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 22, Title = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules", LessonType = LessonType.Video,
                Video = LessonVideos[21],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 23, Title = "Intermolecular Forces", LessonType = LessonType.Video,
                Video = LessonVideos[22],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 24, Title = "Determining Molecular Shape using the VSEPR Theory", LessonType = LessonType.Video,
                Video = LessonVideos[23],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14] } },
            new Lesson() { Id = 25, Title = "Energy and Chemical Change", LessonType = LessonType.Video,
                Video = LessonVideos[24],
                Tags = new HashSet<Tag>(){ Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14] } },
        };

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
                await SyncEntityAsync(TagCategories);
                await SyncEntityAsync(Tags);
                await SyncEntityAsync(IntroVideos.Union(LessonVideos), customUpsert: SyncIntroVideosAsync);
                await SyncEntityAsync(Lessons, include: $"{nameof(Lesson.Tags)}", SyncLessonTagsAsynnc);

                Console.WriteLine("Committing transaction.");
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                Console.WriteLine("Rolling back due to error.");
                await transaction.RollbackAsync();
                throw;
            }
            return this;
        }

        private async Task SetIdentityInsertAsync<TEntity>(bool enabled) where TEntity : BaseEntity
        {
            var entityType = dataContext.Model.FindEntityType(typeof(TEntity)) ?? throw new InvalidOperationException($"Entity type {typeof(TEntity)} not found in model.");
            var sql = $"SET IDENTITY_INSERT [{entityType.GetSchema() ?? "dbo"}].[{entityType.GetTableName()}] {(enabled ? "ON" : "OFF")}";
            await dataContext.Database.ExecuteSqlRawAsync(sql);
        }

        private async Task SyncEntityAsync<TEntity>(IEnumerable<TEntity> source, string? include = null, Func<TEntity, TEntity?, Task>? customUpsert = null) where TEntity : BaseEntity
        {
            Console.WriteLine($"\tSyncing {typeof(TEntity)} entities.");

            await SetIdentityInsertAsync<TEntity>(true);

            var dbSet = dataContext.Set<TEntity>();
            var query = dbSet.TagWithCallSite();
            if (!string.IsNullOrEmpty(include))
            {
                query = query.Include(include);
            }
            var existing = await query.ToListAsync();

            foreach (var item in source)
            {
                var existingItem = existing.FirstOrDefault(l => l.Id == item.Id);

                if (existingItem == null)
                {
                    Console.WriteLine($"\t\tAdding {typeof(TEntity)} with Id {item.Id}.");
                    dbSet.Add(item);
                }
                else
                {
                    Console.WriteLine($"\t\tUpdating {typeof(TEntity)} with Id {item.Id}.");
                    dataContext.Entry(existingItem).CurrentValues.SetValues(item);
                }

                if (customUpsert != null)
                {
                    await customUpsert(item, existingItem);
                }
            }

            foreach (var existingItem in existing)
            {
                if (!source.Any(l => l.Id == existingItem.Id))
                {
                    Console.WriteLine($"\t\tRemoving {typeof(TEntity)} with Id {existingItem.Id}.");
                    dbSet.Remove(existingItem);
                }
            }

            Console.WriteLine("Saving changes.");
            await dataContext.SaveChangesAsync();
            await SetIdentityInsertAsync<TEntity>(false);
        }

        private async Task SyncLessonTagsAsynnc(Lesson source, Lesson? target)
        {
            if (target == null)
                return;

            var existingTags = await dataContext.Tags.ToListAsync();

            foreach (var tag in source.Tags)
            {
                var existingTag = existingTags.Single(x => x.Id == tag.Id);
                if (!target.Tags.Contains(existingTag))
                {
                    target.Tags.Add(existingTag);
                }
            }

            foreach (var tag in target.Tags)
            {
                if (!source.Tags.Any(sourceTag => sourceTag.Id == tag.Id))
                {
                    target.Tags.Remove(tag);
                }
            }
        }

        private async Task SyncIntroVideosAsync(Video source, Video? target)
        {
            if (target == null || source.Intro == null)
                return;

            var existingIntros = await dataContext.Videos.Where(video => video.VideoType == VideoType.Intro).ToListAsync();

            if (target.VideoType == VideoType.FullLesson)
            {
                var existingIntro = existingIntros.Single(video => video.Id == source.Intro.Id);
                target.Intro = existingIntro;
            }
        }
    }
}
