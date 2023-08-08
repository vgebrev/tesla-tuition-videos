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
            Console.WriteLine("Running migrations.");
            await dataContext.Database.MigrateAsync();
            if (dataContext.Courses.Any())
            {
                Console.WriteLine("Courses already exist, will not populate.");
                return this;
            }

            var courses = new List<Course>()
            {
                new Course() 
                { 
                    Title = "Grade 11 IEB Electromagnetism Physics Course", 
                    Description = "This is the description of an example grade 11 physics test course",
                    Chapters = new HashSet<Chapter>()
                    {
                        new Chapter()
                        {
                            SequenceNumber = 1,
                            Title = "Chapter 1: Don't put toasters in the bath tub",
                            Description = "Description of the example chapter 1",
                            Lessons = new HashSet<Lesson>()
                            {
                                new Lesson()
                                {
                                    SequenceNumber = 1,
                                    Title = "Video 1.1: Turbines go brrrrrr",
                                    Description = "Description of the example video lesson",
                                    LessonType = LessonType.Video,
                                },
                                new Lesson()
                                {
                                    SequenceNumber = 2,
                                    Title = "Worksheet 1.1: Do your homework!",
                                    Description = "Description of the example worksheet lesson",
                                    LessonType = LessonType.Worksheet,
                                }
                            }
                        },
                        new Chapter()
                        {
                            SequenceNumber = 2,
                            Title = "Chapter 2: Magnets how do they work?",
                            Description = "We'll probably never know",
                            Lessons = new HashSet<Lesson>()
                            {
                                new Lesson()
                                {
                                    SequenceNumber = 1,
                                    Title = "Video 2.1: Opposites attract",
                                    Description = "Description of the second example video lesson",
                                    LessonType = LessonType.Video,
                                },
                            }
                        }
                    }
                },
                new Course() 
                { 
                    Title = "Grade 12 CAPS Organic Chemistry Course", 
                    Description = "This is the description of an example grade 12 chemistry course" 
                },
                new Course() 
                { 
                    Title = "Grade 12 CAPS Momentum and Impulse Physics Course", 
                    Description = "This is the description of the grade 12 chemi course" ,
                    Chapters = new HashSet<Chapter>()
                    {
                        new Chapter()
                        {
                            SequenceNumber = 1,
                            Title = "Chapter 1: Cannons and Ballistic Missiles",
                            Description = "Learn about blowing things up from a safe distance",
                            Lessons = new HashSet<Lesson>()
                            {
                                new Lesson()
                                {
                                    SequenceNumber = 1,
                                    Title = "Video 1.1: Cannon goes BOOM!",
                                    Description = "Gun powder was invented in China",
                                    LessonType = LessonType.Video,
                                },
                            }
                        }
                    }
                }
            };

            foreach (var course in courses)
            {
                Console.WriteLine($"Adding course {course.Description}.");
                dataContext.Courses.Add(course);
            }
            Console.WriteLine("Saving changes.");
            await dataContext.SaveChangesAsync();
            return this;
        }
    }
}
