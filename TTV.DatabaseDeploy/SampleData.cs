using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Drawing;
using TTV.Domain.Entities;
using TTV.Infrastructure.DataAccess;

namespace TTV.DatabaseDeploy
{
    internal class SampleData(DataContext dataContext)
    {
        private readonly DataContext dataContext = dataContext;
        private static TagCategory[] TagCategories { get; } =
        [
            new TagCategory() { Id = 1, Name = "Paper", Priority = 0 },
            new TagCategory() { Id = 2, Name = "Topic", Priority = 1 },
            new TagCategory() { Id = 3, Name = "Syllabus", Priority = 2 },
            new TagCategory() { Id = 4, Name = "Other", Priority = 3}
        ];

        private static Tag[] Tags { get; } =
        [
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
            new Tag() { Id = 13, Name = "CAPS", Category = TagCategories[2] },

            // Other
            new Tag() { Id = 14, Name = "Grade 10", Category = TagCategories[3] },
            new Tag() { Id = 15, Name = "Grade 11", Category = TagCategories[3] },
            new Tag() { Id = 16, Name = "Grade 12", Category = TagCategories[3] },
            new Tag() { Id = 17, Name = "Vectors", Category = TagCategories[3] },
            new Tag() { Id = 18, Name = "Forces", Category = TagCategories[3] },
            new Tag() { Id = 19, Name = "Newton's Laws", Category = TagCategories[3] },

            // Update 2024-06-01
            // Topic
            new Tag() { Id = 20, Name = "Electromagnetism", Category = TagCategories[1] },
            new Tag() { Id = 21, Name = "Types of Reactions", Category = TagCategories[1] },
            new Tag() { Id = 22, Name = "Electrochemistry", Category = TagCategories[1] },
            new Tag() { Id = 23, Name = "Redox Reactions", Category = TagCategories[1] },
            new Tag() { Id = 24, Name = "Acids & Bases", Category = TagCategories[1] },
            new Tag() { Id = 25, Name = "Ideal Gases and Thermal Properties", Category = TagCategories[1] },
            new Tag() { Id = 26, Name = "Basic Chemistry", Category = TagCategories[1] },
            
            // Other
            new Tag() { Id = 27, Name = "Compounds", Category = TagCategories[3] },
            new Tag() { Id = 28, Name = "Ions", Category = TagCategories[3] },
            new Tag() { Id = 29, Name = "Chemical Formulae", Category = TagCategories[3] },
            new Tag() { Id = 30, Name = "Free Lesson", Category = TagCategories[3]},
        ];

        private static Lesson[] Lessons { get; } =
        [
            // Physics
            new Lesson() { Id = 1, Title = "Finding the Resultant of Multiple Forces", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 2, Title = "Components of Angled Forces", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 3, Title = "Forces in Equilibrium", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 4, Title = "Forces on an Inclined Surface", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 5, Title = "The Force of Normal", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 6, Title = "Frictional Forces", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 7, Title = "Coefficients of Friction", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 8, Title = "Newton's First Law of Motion", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 9, Title = "Newton's Second Law of Motion", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 10, Title = "Newton's 2nd Law Questions Involving Simultaneous Equations", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 11, Title = "Newton's Third Law of Motion", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 12, Title = "Newton's Law of Universal Gravitation", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 13, Title = "Electrostatics Part 1", Description = "Revision of basics, Coulomb's Law (theory and calculations.)", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[4], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 14, Title = "Electrostatics Part 2", Description = "Electric fields, electric field strength at a point (theory and calculations), other electrostatics calculations.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[4], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 15, Title = "Electric Circuits Part 1", Description = "Potential difference, current, resistance, EMF, ammeters, voltmeters, Ohm’s Law, Ohmic vs. non-Ohmic conductors.",LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 16, Title = "Electric Circuits Part 2", Description = "Series circuits, parallel circuits and combination circuits.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 17, Title = "Electric Circuits Part 3", Description = "The effect of adding/removing resistors in series/parallel, electrical power, cost of electricity using kilowatthours, electric circuits calculations.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 18, Title = "Electromagnetism (CAPS)", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[12], Tags[14], Tags[19]] },
            new Lesson() { Id = 19, Title = "Electromagnetism (IEB)", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[11], Tags[14], Tags[19]] },
            
            // Chemistry
            new Lesson() { Id = 20, Title = "Converting Moles Between Different Substances in a Chemical Reaction", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 21, Title = "Limiting Reagents", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 22, Title = "Percentage Purity", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 23, Title = "Percentage Yield", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 24, Title = "Quantitative Aspects of Chemical Change", Description = "Basic formulae and calculations", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 25, Title = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 26, Title = "Intermolecular Forces", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 27, Title = "Determining Molecular Shape using the VSEPR Theory", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 28, Title = "Dative Covalent Bonding (CAPS)", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[12], Tags[14]] },
            new Lesson() { Id = 29, Title = "Energy and Chemical Change", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 30, Title = "Redox Reactions Part 1 (CAPS)", Description = "Basic redox chemistry, using the table of standard reduction potentials, writing and balancing half reactions, net ionic equations and spontaneous redox reactions", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[12], Tags[14]] },
            new Lesson() { Id = 31, Title = "Redox Reactions Part 2 (CAPS)", Description = "Oxidation numbers, examples of redox reaction questions", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[12], Tags[14]] },
            new Lesson() { Id = 32, Title = "Redox Reactions (IEB)", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[11], Tags[14]] },
            new Lesson() { Id = 33, Title = "Acids & Bases Part 1 (CAPS)", Description = "Basic acid-base chemistry, the Arrhenius and Brønsted-Lowry theories, conjugate acid-base pairs, ampholytes, monoprotic vs. polyprotic acids", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[12], Tags[14]] },
            new Lesson() { Id = 34, Title = "Acids & Bases Part 2 (CAPS)", Description = "Neutralisation reactions, brief overview of titrations, examples of acid-base calculations", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[12], Tags[14]] },
            new Lesson() { Id = 35, Title = "Acids & Bases Part 1 (IEB)", Description = "Basic acid-base chemistry, the Lowry- Brønsted theory, conjugate acid-base pairs, amphoteric substances, strong vs. weak acids and bases", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[11], Tags[14]] },
            new Lesson() { Id = 36, Title = "Ideal Gases (CAPS)", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[24], Tags[12], Tags[14]] },
            new Lesson() { Id = 37, Title = "Ions, Valency & Writing Molecular Formulae", IsFree = true, LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[11], Tags[12], Tags[14], Tags[25], Tags[26], Tags[27], Tags[28], Tags[29]] },

        ];

        private static Video[] IntroVideos { get; } =
        [
            // Physics
            new Video() { Id = 1, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[0].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[0] },
            new Video() { Id = 2, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[1].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[1] },
            new Video() { Id = 3, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[2].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[2] },
            new Video() { Id = 4, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[3].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[3] },
            new Video() { Id = 5, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[4].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[4] },
            new Video() { Id = 6, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[5].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[5] },
            new Video() { Id = 7, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[6].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[6] },
            new Video() { Id = 8, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[7].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[7] },
            new Video() { Id = 9, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[8].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[8] },
            new Video() { Id = 10, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[9].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[9]  },
            new Video() { Id = 11, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[10].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[10] },
            new Video() { Id = 12, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[11].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[11] },
            new Video() { Id = 13, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[12].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[12] },
            new Video() { Id = 14, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[13].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[13] },
            new Video() { Id = 15, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[14].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[14] },
            new Video() { Id = 16, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[15].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[15] },
            new Video() { Id = 17, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[16].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[16] },
            new Video() { Id = 18, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[17].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[17] },
            // Chemistry
            new Video() { Id = 19, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[18].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[18] },
            new Video() { Id = 20, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[19].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[19] },
            new Video() { Id = 21, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[20].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[20] },
            new Video() { Id = 22, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[21].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[21] },
            new Video() { Id = 23, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[22].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[22] },
            new Video() { Id = 24, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[23].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[23] },
            new Video() { Id = 25, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[24].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[24] },
            new Video() { Id = 26, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[25].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[25] },
            new Video() { Id = 27, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[26].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[26] },
            new Video() { Id = 28, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[27].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[27] },
            new Video() { Id = 29, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[28].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[28] },
            new Video() { Id = 30, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[29].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[29] },
            new Video() { Id = 31, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[30].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[30] },
            new Video() { Id = 32, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[31].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[31] },
            new Video() { Id = 33, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[32].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[32] },
            new Video() { Id = 34, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[33].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[33] },
            new Video() { Id = 35, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[34].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[34] },
            new Video() { Id = 36, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[35].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[35] },
            new Video() { Id = 37, Filename = "TTV-Intro-Placeholder.mp4", Thumbnail = $"{Lessons[36].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[36] },
        ];

        private static Video[] LessonVideos { get; } =
        [
            // Physics
            new Video() { Id = 38, Filename = $"{Lessons[0].Title}.mp4", Thumbnail = $"{Lessons[0].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[0] },
            new Video() { Id = 39, Filename = $"{Lessons[1].Title}.mp4", Thumbnail = $"{Lessons[1].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[1] },
            new Video() { Id = 40, Filename = $"{Lessons[2].Title}.mp4", Thumbnail = $"{Lessons[2].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[2] },
            new Video() { Id = 41, Filename = $"{Lessons[3].Title}.mp4", Thumbnail = $"{Lessons[3].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[3] },
            new Video() { Id = 42, Filename = $"{Lessons[4].Title}.mp4", Thumbnail = $"{Lessons[4].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[4] },
            new Video() { Id = 43, Filename = $"{Lessons[5].Title}.mp4", Thumbnail = $"{Lessons[5].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[5] },
            new Video() { Id = 44, Filename = $"{Lessons[6].Title}.mp4", Thumbnail = $"{Lessons[6].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[6] },
            new Video() { Id = 45, Filename = $"{Lessons[7].Title}.mp4", Thumbnail = $"{Lessons[7].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[7] },
            new Video() { Id = 46, Filename = $"{Lessons[8].Title}.mp4", Thumbnail = $"{Lessons[8].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[8] },
            new Video() { Id = 47, Filename = $"{Lessons[9].Title}.mp4", Thumbnail = $"{Lessons[9].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[9] },
            new Video() { Id = 48, Filename = $"{Lessons[10].Title}.mp4", Thumbnail = $"{Lessons[10].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[10] },
            new Video() { Id = 49, Filename = $"{Lessons[11].Title}.mp4", Thumbnail = $"{Lessons[11].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[11] },
            new Video() { Id = 50, Filename = $"{Lessons[12].Title}.mp4", Thumbnail = $"{Lessons[12].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[12] },
            new Video() { Id = 51, Filename = $"{Lessons[13].Title}.mp4", Thumbnail = $"{Lessons[13].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[13] },
            new Video() { Id = 52, Filename = $"{Lessons[14].Title}.mp4", Thumbnail = $"{Lessons[14].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[14] },
            new Video() { Id = 53, Filename = $"{Lessons[15].Title}.mp4", Thumbnail = $"{Lessons[15].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[15] },
            new Video() { Id = 54, Filename = $"{Lessons[16].Title}.mp4", Thumbnail = $"{Lessons[16].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[16] },
            new Video() { Id = 55, Filename = $"{Lessons[17].Title}.mp4", Thumbnail = $"{Lessons[17].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[17] },
            // Chemistry
            new Video() { Id = 56, Filename = $"{Lessons[18].Title}.mp4", Thumbnail = $"{Lessons[18].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[18] },
            new Video() { Id = 57, Filename = $"{Lessons[19].Title}.mp4", Thumbnail = $"{Lessons[19].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[19] },
            new Video() { Id = 58, Filename = $"{Lessons[20].Title}.mp4", Thumbnail = $"{Lessons[20].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[20] },
            new Video() { Id = 59, Filename = $"{Lessons[21].Title}.mp4", Thumbnail = $"{Lessons[21].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[21] },
            new Video() { Id = 60, Filename = $"{Lessons[22].Title}.mp4", Thumbnail = $"{Lessons[22].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[22] },
            new Video() { Id = 61, Filename = $"{Lessons[23].Title}.mp4", Thumbnail = $"{Lessons[23].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[23] },
            new Video() { Id = 62, Filename = $"{Lessons[24].Title}.mp4", Thumbnail = $"{Lessons[24].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[24] },
            new Video() { Id = 63, Filename = $"{Lessons[25].Title}.mp4", Thumbnail = $"{Lessons[25].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[25] },
            new Video() { Id = 64, Filename = $"{Lessons[26].Title}.mp4", Thumbnail = $"{Lessons[26].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[26] },
            new Video() { Id = 65, Filename = $"{Lessons[27].Title}.mp4", Thumbnail = $"{Lessons[27].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[27] },
            new Video() { Id = 66, Filename = $"{Lessons[28].Title}.mp4", Thumbnail = $"{Lessons[28].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[28] },
            new Video() { Id = 67, Filename = $"{Lessons[29].Title}.mp4", Thumbnail = $"{Lessons[29].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[29] },
            new Video() { Id = 68, Filename = $"{Lessons[30].Title}.mp4", Thumbnail = $"{Lessons[30].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[30] },
            new Video() { Id = 69, Filename = $"{Lessons[31].Title}.mp4", Thumbnail = $"{Lessons[31].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[31] },
            new Video() { Id = 70, Filename = $"{Lessons[32].Title}.mp4", Thumbnail = $"{Lessons[32].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[32] },
            new Video() { Id = 71, Filename = $"{Lessons[33].Title}.mp4", Thumbnail = $"{Lessons[33].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[33] },
            new Video() { Id = 72, Filename = $"{Lessons[34].Title}.mp4", Thumbnail = $"{Lessons[34].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[34] },
            new Video() { Id = 73, Filename = $"{Lessons[35].Title}.mp4", Thumbnail = $"{Lessons[35].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[35] },
            new Video() { Id = 74, Filename = $"{Lessons[36].Title}.mp4", Thumbnail = $"{Lessons[36].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[36] },
        ];

        private static Price[] Prices { get; } =
        [
            // Physics
            new Price() { Id = 1, Amount = 80, Lesson = Lessons[0] },
            new Price() { Id = 2, Amount = 80, Lesson = Lessons[1] },
            new Price() { Id = 3, Amount = 80, Lesson = Lessons[2] },
            new Price() { Id = 4, Amount = 80, Lesson = Lessons[3] },
            new Price() { Id = 5, Amount = 60, Lesson = Lessons[4] },
            new Price() { Id = 6, Amount = 80, Lesson = Lessons[5] },
            new Price() { Id = 7, Amount = 80, Lesson = Lessons[6] },
            new Price() { Id = 8, Amount = 80, Lesson = Lessons[7] },
            new Price() { Id = 9, Amount = 80, Lesson = Lessons[8] },
            new Price() { Id = 10, Amount = 80, Lesson = Lessons[9] },
            new Price() { Id = 11, Amount = 60, Lesson = Lessons[10] },
            new Price() { Id = 12, Amount = 100, Lesson = Lessons[11] },
            new Price() { Id = 13, Amount = 100, Lesson = Lessons[12] },
            new Price() { Id = 14, Amount = 100, Lesson = Lessons[13] },
            new Price() { Id = 15, Amount = 80, Lesson = Lessons[14] },
            new Price() { Id = 16, Amount = 80, Lesson = Lessons[15] },
            new Price() { Id = 17, Amount = 100, Lesson = Lessons[16] },
            new Price() { Id = 18, Amount = 100, Lesson = Lessons[17] },
            new Price() { Id = 19, Amount = 100, Lesson = Lessons[18] },
            
            // Chemistry
            new Price() { Id = 20, Amount = 60, Lesson = Lessons[19] },
            new Price() { Id = 21, Amount = 80, Lesson = Lessons[20] },
            new Price() { Id = 22, Amount = 80, Lesson = Lessons[21] },
            new Price() { Id = 23, Amount = 80, Lesson = Lessons[22] },
            new Price() { Id = 24, Amount = 100, Lesson = Lessons[23] },
            new Price() { Id = 25, Amount = 80, Lesson = Lessons[24] },
            new Price() { Id = 26, Amount = 100, Lesson = Lessons[25] },
            new Price() { Id = 27, Amount = 60, Lesson = Lessons[26] },
            new Price() { Id = 28, Amount = 60, Lesson = Lessons[27] },
            new Price() { Id = 29, Amount = 100, Lesson = Lessons[28] },
            new Price() { Id = 30, Amount = 80, Lesson = Lessons[29] },
            new Price() { Id = 31, Amount = 80, Lesson = Lessons[30] },
            new Price() { Id = 32, Amount = 100, Lesson = Lessons[31] },
            new Price() { Id = 33, Amount = 80, Lesson = Lessons[32] },
            new Price() { Id = 34, Amount = 100, Lesson = Lessons[33] },
            new Price() { Id = 35, Amount = 100, Lesson = Lessons[34] },
            new Price() { Id = 36, Amount = 100, Lesson = Lessons[35] },
            new Price() { Id = 37, Amount = 0, Lesson = Lessons[36] },
        ];

        private static Document[] Documents { get; set; } =
        [
            new Document() { Id = 1, DocumentType = DocumentType.ExercisePdf, Title = $"Components of Angled Forces", Filename = $"Components of Angled Forces.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[1]] },
            new Document() { Id = 2, DocumentType = DocumentType.ExercisePdf, Title = $"Electric Circuits Part 1", Filename = $"Electric Circuits Part 1.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[14]] },
            new Document() { Id = 3, DocumentType = DocumentType.ExercisePdf, Title = $"Electric Circuits Parts 2 and 3", Filename = $"Electric Circuits Parts 2 and 3.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[15], Lessons[16]] },
            new Document() { Id = 4, DocumentType = DocumentType.ExercisePdf, Title = $"Finding the Resultant of Multiple Forces", Filename = $"Finding the Resultant of Multiple Forces.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[0]] },
            new Document() { Id = 5, DocumentType = DocumentType.ExercisePdf, Title = $"Forces in Equilibrium", Filename = $"Forces in Equilibrium.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[2]] },
            new Document() { Id = 6, DocumentType = DocumentType.ExercisePdf, Title = $"Forces on an Inclined Surface", Filename = $"Forces on an Inclined Surface.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[3]] },
            new Document() { Id = 7, DocumentType = DocumentType.ExercisePdf, Title = $"Frictional Forces", Filename = $"Frictional Forces.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[5]] },
            new Document() { Id = 8, DocumentType = DocumentType.ExercisePdf, Title = $"Limiting Reagents", Filename = $"Limiting Reagents.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[20]] },
            new Document() { Id = 9, DocumentType = DocumentType.ExercisePdf, Title = $"Newton's 1st Law of Motion", Filename = $"Newton's 1st Law of Motion.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[7]] },
            new Document() { Id = 10, DocumentType = DocumentType.ExercisePdf, Title = $"Newton's 2nd Law of Motion", Filename = $"Newton's 2nd Law of Motion.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[8]] },
            new Document() { Id = 11, DocumentType = DocumentType.ExercisePdf, Title = $"Newton's 2nd Law Questions Involving Simultaneous Equations", Filename = $"Newton's 2nd Law Questions Involving Simultaneous Equations.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[9]] },
            new Document() { Id = 12, DocumentType = DocumentType.ExercisePdf, Title = $"Newton's 3rd Law of Motion", Filename = $"Newton's 3rd Law of Motion.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[10]] },
            new Document() { Id = 13, DocumentType = DocumentType.ExercisePdf, Title = $"Newton's Law of Universal Gravitation", Filename = $"Newton's Law of Universal Gravitation.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[11]] },
            new Document() { Id = 14, DocumentType = DocumentType.ExercisePdf, Title = $"Percentage Purity", Filename = $"Percentage Purity.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[21]] },
            new Document() { Id = 15, DocumentType = DocumentType.ExercisePdf, Title = $"Percentage Yield", Filename = $"Percentage Yield.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[22]] },
            new Document() { Id = 16, DocumentType = DocumentType.ExercisePdf, Title = $"The Force of Normal", Filename = $"The Force of Normal.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[4]] },
            new Document() { Id = 17, DocumentType = DocumentType.ExercisePdf, Title = $"Acids and Bases (CAPS) Part 1", Filename = $"Acids and Bases (CAPS) Part 1.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[32]] },
            new Document() { Id = 18, DocumentType = DocumentType.ExercisePdf, Title = "Coefficients of Friction", Filename = "Coefficients of Friction.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[6]] },
            new Document() { Id = 19, DocumentType = DocumentType.ExercisePdf, Title = "Converting Moles Between Different Substances in a Chemical Reaction", Filename = "Converting Moles between Different Substances in a Chemical Reaction.pdf", RelativePath="ExercisePdfs", Lessons = [Lessons[19]] },
            new Document() { Id = 20, DocumentType = DocumentType.ExercisePdf, Title = "Electrostatics Part 1", Filename = "Electrostatics Part 1.pdf", RelativePath="ExercisePdfs", Lessons = [Lessons[12]] },
            new Document() { Id = 21, DocumentType = DocumentType.ExercisePdf, Title = "Electrostatics Part 2", Filename = "Electrostatics Part 2.pdf", RelativePath="ExercisePdfs", Lessons = [Lessons[13]] },
            new Document() { Id = 22, DocumentType = DocumentType.ExercisePdf, Title = "Ions, Valency & Writing Molecular Formulae", Filename = "Ions, Valency & Writing Molecular Formulae.pdf", RelativePath="ExercisePdfs", Lessons = [Lessons[36]] },
            new Document() { Id = 23, DocumentType = DocumentType.ExercisePdf, Title = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules", Filename = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[24]]},
            new Document() { Id = 24, DocumentType = DocumentType.ExercisePdf, Title = "Quantitative Aspects of Chemical Change", Filename = "Quantitative Aspects of Chemical Change.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[23]] },
        ];
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
                await SyncEntityAsync(Lessons, include: $"{nameof(Lesson.Tags)}", SyncLessonChildEntitiesAsync);
                await SyncEntityAsync(IntroVideos.Union(LessonVideos), customUpsert: SyncVideoLessonsAsync);
                await SyncEntityAsync(Documents, include: $"{nameof(Document.Lessons)}", customUpsert: SyncDocumentLessonsAsync);
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

                if (customUpsert != null)
                {
                    await customUpsert(item, existingItem);
                }

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

        private async Task SyncLessonChildEntitiesAsync(Lesson source, Lesson? target)
        {
            if (target == null)
            {
                return;
            }

            // Prices
            var prices = Prices.Where(price => price.Lesson.Id == target.Id);
            target.Prices.Clear();
            foreach (var price in prices)
            {
                target.Prices.Add(new Price() { Amount = price.Amount, PromoAmount = price.PromoAmount, EffectiveDate = price.EffectiveDate, Lesson = target });
            }

            // Tags
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

            //Documents
            target.Documents.Clear();
        }

        private async Task SyncVideoLessonsAsync(Video source, Video? target)
        {
            if (target == null)
                return;

            var existingLessons = await dataContext.Lessons.ToListAsync();
            target.Lesson = existingLessons.Single(x => x.Id == source.Lesson.Id);
        }

        private async Task SyncDocumentLessonsAsync(Document source, Document? target)
        {
            target ??= source;
            var existingLessons = await dataContext.Lessons.ToListAsync();

            if (target != source)
            {
                target.Lessons.Clear();
            }
            foreach (var lesson in source.Lessons)
            {
                target.Lessons.Add(existingLessons.Single(existingLesson => existingLesson.Id == lesson.Id));
            }
        }
    }
}
