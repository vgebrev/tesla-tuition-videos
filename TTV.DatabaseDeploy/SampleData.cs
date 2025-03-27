using Microsoft.EntityFrameworkCore;
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

            //Update 2025-01-31
            new Tag() { Id = 31, Name = "Electrodynamics", Category = TagCategories[1] },
            new Tag() { Id = 32, Name = "Organic Chemistry", Category = TagCategories[1] },

            //Update 2025-03-27
            new Tag() { Id = 33, Name = "Homologous Series", Category = TagCategories[3] },
            new Tag() { Id = 34, Name = "Functional Group", Category = TagCategories[3] },
            new Tag() { Id = 35, Name = "Carbon Compounds", Category = TagCategories[3] },
            new Tag() { Id = 36, Name = "IUPAC Name", Category = TagCategories[3] },
            new Tag() { Id = 37, Name = "Hydrocarbon", Category = TagCategories[3] },
            new Tag() { Id = 38, Name = "Alkanes", Category = TagCategories[3] },
            new Tag() { Id = 39, Name = "Haloalkanes", Category = TagCategories[3] },
        ];

        private static Lesson[] Lessons { get; } =
        [
            // Physics
            new Lesson() { Id = 1, Sequence = 1, Title = "Finding the Resultant of Multiple Forces", Description = "Learn how to correctly calculate the magnitude and direction of the resultant/net force when multiple forces act on a single point/object. This also involves breaking down angled forces into their x (horizontal) and y (vertical) components.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 2, Sequence = 2, Title = "Components of Angled Forces", Description = "Forces at an angle are important for many parts of Physics. Learn how they work and how to resolve them (break them down) into their x (horizontal) and y (vertical) components with the relevant formulae.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 3, Sequence = 3, Title = "Forces in Equilibrium", Description = "Learn about what it means for objects to be in equilibrium when the forces acting on them are balanced. This involves techniques from Newton's 1 st Law of Motion and covers examples of equilibrium questions and calculations.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 4, Sequence = 4, Title = "Forces on an Inclined Surface", Description = "Learn about how forces work on a slope or incline. This includes how to correctly draw free-body diagrams and the formulae used for calculating the parallel and perpendicular components of an object's weight/force of gravity on a slope.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 5, Sequence = 5, Title = "The Force of Normal", Description = "Learn about what the normal force is, how to represent it on diagrams and how to calculate it correctly in a variety of different scenarios (including in the presence of angled forces and on an incline/slope).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 6, Sequence = 6, Title = "Frictional Forces", Description = "Learn about the important concepts related to friction, including the difference between static and kinetic friction. This includes how each type of friction works and how to navigate questions that involve frictional forces.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 7, Sequence = 7, Title = "Coefficients of Friction", Description = "Learn about what a coefficient of friction is and the difference between the coefficient of static friction and the coefficient of kinetic friction. This includes learning about the formulae, calculations and other questions related to coefficients of friction (with examples).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 8, Sequence = 8, Title = "Newton's First Law of Motion", Description = "This lesson deals with the theory and concepts related to Newton's 1st Law of Motion. This includes an explanation of inertia, equilibrium and the techniques used for solving Newton's 1st Law questions (including examples of calculations).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 9, Sequence = 9, Title = "Newton's Second Law of Motion", Description = "This lesson teaches students about the theory and relationships related to Newton's 2nd Law of motion. This includes the formula and techniques needed to navigate Newton's 2nd Law calculations and other related questions.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 10, Sequence = 10, Title = "Newton's 2nd Law Questions Involving Simultaneous Equations", Description = "Learn about how to navigate the tricky but very common Newton's 2nd Law questions that involve simultaneous equations. This includes the techniques for identifying, interpreting and solving these sorts of questions correctly.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 11, Sequence = 11, Title = "Newton's Third Law of Motion", Description = "Learn about the theory and concepts related to Newton's 3rd Law of motion. This includes identifying Newton's 3rd Law force pairs (or action/reaction force pairs) and dealing with various types of questions related to Newton's 3rd Law.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 12, Sequence = 12, Title = "Newton's Law of Universal Gravitation", Description = "This lesson deals with the theory, concepts and relationships related to gravitation on a universal scale. This includes learning how to navigate a variety of questions (including calculations, proportionality questions, and calculating acceleration due to gravity/gravitational field strength on different planets).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[2], Tags[3], Tags[11], Tags[12], Tags[14], Tags[16], Tags[17], Tags[18]] },
            new Lesson() { Id = 13, Sequence = 13, Title = "Electrostatics Part 1", Description = "This lesson revises the basic but fundamental concepts of electrostatics and thoroughly explores Coulomb's Law. This includes the theory, relationships and calculations related to Coulomb's Law (with multiple examples).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[4], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 14, Sequence = 14, Title = "Electrostatics Part 2", Description = "In this lesson we learn about electric fields (including the diagrams), electric field strength at a point (theory and calculations) and other types of electrostatics calculations (including proportionality questions, charged objects touching and separating and the quantisation of charge).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[4], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 15, Sequence = 15, Title = "Electric Circuits Part 1", Description = "Learn the fundamental electricity concepts of potential difference, current, resistance and EMF. Also learn about how ammeters and voltmeters work. This lesson also includes the theory related to Ohm's Law (including Ohmic vs. non-Ohmic conductors).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 16, Sequence = 16, Title = "Electric Circuits Part 2", Description = "This lesson teaches the important concepts related to series circuits, parallel circuits and combination circuits. This includes learning about how the different arrangement of resistors affects current, potential difference and total resistance in the circuit and gives techniques for navigating electric circuit diagrams and questions.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 17, Sequence = 17, Title = "Electric Circuits Part 3", Description = "Learn about the effect of adding/removing resistors in series/parallel, electrical power and energy, and how to calculate the cost of electricity using kilowatthours. This lesson also includes worked examples of electric circuit calculations.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[5], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 18, Sequence = 18, Title = "Electromagnetism (CAPS)", Description = "In this lesson we learn about the magnetic fields induced around current-carrying conductors (including straight wires, circular coils and solenoids). This includes the Right Hand Wire Rule and Right Hand Solenoid Rule. We also learn about Faraday's Law of Electromagnetic Induction (including the concepts of magnetic flux, moving bar-magnets in/out of solenoids, etc.).", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[12], Tags[14], Tags[19]] },
            new Lesson() { Id = 19, Sequence = 19, Title = "Electromagnetism (IEB)", Description = "In this lesson we learn about the magnetic fields around permanent magnets and those induced around current-carrying conductors (including straight wires, circular coils and solenoids). This includes the Right Hand Wire Rule and Right Hand Solenoid Rule. We also learn about the Motor Effect, where a force acts on current-carrying conductors placed in a magnetic field.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[11], Tags[14], Tags[19]] },
            
            // Chemistry
            new Lesson() { Id = 20, Sequence = 21, Title = "Converting Moles Between Different Substances in a Chemical Reaction", Description = "This is an essential skill required for a large variety of different stoichiometric calculations. In this lesson I teach an efficient and fool-proof method for correctly converting mole ratios between different substances in a chemical reaction.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 21, Sequence = 22, Title = "Limiting Reagents", Description = "Learn about the important concepts of limiting and excess reagents. This includes the steps and techniques needed to correctly identify the limiting reagent in a chemical reaction. This lesson also demonstrates examples of calculations related to the limiting and excess reagents, which is important for other types of calculations as well.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 22, Sequence = 23, Title = "Percentage Purity", Description = "Learn about the concepts and theory related to percentage purity. This includes a comparison of pure and impure substances, the formula needed to calculate percentage purity, and multiple worked examples of calculations.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 23, Sequence = 24, Title = "Percentage Yield", Description = "Learn about what percentage yield means (including the concepts of actual yield and theoretical yield). This lesson covers the percentage yield formula and various worked examples of calculations related to percentage yield.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 24, Sequence = 25, Title = "Quantitative Aspects of Chemical Change", Description = "Learn about fundamental concepts of stoichiometry including the mole, Avogadro's number, molecular mass and the various formulae used to convert between mass, number of particles, volume, concentration, etc. This lesson also demonstrates calculations using these formulae.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[6], Tags[7], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 25, Sequence = 26, Title = "Polar and Non-Polar Bonds vs Polar and Non-Polar Molecules", Description = "Learn the difference between polar and non-polar bonds as well as polar and non-polar molecules. This includes the steps required to correctly determine if a molecule is polar or non-polar which is essential for the section of intermolecular forces.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 26, Sequence = 27, Title = "Intermolecular Forces", Description = "This lesson covers the difference between intermolecular forces and intramolecular bonds. We also learn about the various types of intermolecular forces (including van der Waal's forces such as dipole-dipole forces, London forces and hydrogen bonding). This also includes an explanation of how the strength and type of intra- and intermolecular forces affect physical properties such as melting and boiling point.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 27, Sequence = 28, Title = "Determining Molecular Shape using the VSEPR Theory", Description = "This lesson explains how the Valence Shell Electron Pair (VSEPR) Theory can be used to predict the three-dimensional shape or geometry of a molecule. This includes a set of steps (using a general formula) used to correctly determine molecular shapes and gives examples of the most common molecular geometries.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 28, Sequence = 29, Title = "Dative Covalent Bonding (CAPS)", Description = "Learn about what a dative-covalent/co-ordinate bond is (including its definition). Also learn how to show the formation of dative covalent bonds in polyatomic ions (such as the hydronium and ammonium ions) using Lewis diagrams.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[12], Tags[14]] },
            new Lesson() { Id = 29, Sequence = 30, Title = "Energy and Chemical Change", Description = "Learn about the energy changes that are involved in all chemical reactions. Also learn about concepts such as bond energy, bond length, change in enthalpy/heat of reaction, catalysts, activation energy, etc. This lesson also explains the differences and characteristics of endothermic and exothermic reactions (including their energy profiles) and the potential energy changes associated with bond formation.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[8], Tags[9], Tags[10], Tags[11], Tags[12], Tags[14]] },
            new Lesson() { Id = 30, Sequence = 31, Title = "Redox Reactions Part 1 (CAPS)", Description = "Learn about the fundamental concepts of redox reactions, including oxidation, reduction, oxidising agents and reducing agents. This includes an in-depth look at the Table of Standard Reduction Potentials and how to use it. We also learn about spontaneous redox reactions, writing and balancing half reactions and writing the net ionic equation.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[12], Tags[14]] },
            new Lesson() { Id = 31, Sequence = 32, Title = "Redox Reactions Part 2 (CAPS)", Description = "This lesson explains the meaning behind oxidation numbers. This includes learning how to assign them, interpret them and use them in redox chemistry. We also look at a variety of different redox reaction questions.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[12], Tags[14]] },
            new Lesson() { Id = 32, Sequence = 33, Title = "Redox Reactions (IEB)", Description = "This lesson covers the basic concepts related to redox reactions, including oxidation, reduction, oxidising agents and reducing agents. This includes learning about the Table of Standard Electrode Potentials and how to use it. We also learn about spontaneous redox reactions, writing and balancing half reactions and writing the net ionic equation.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[21], Tags[22], Tags[11], Tags[14]] },
            new Lesson() { Id = 33, Sequence = 34, Title = "Acids & Bases Part 1 (CAPS)", Description = "This lesson teaches the basic theory and concepts of acid-base chemistry. This includes the Arrhenius and Brønsted-Lowry theories, common acids and bases, conjugate acid-base pairs, ampholytes and monoprotic vs. polyprotic acids.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[12], Tags[14]] },
            new Lesson() { Id = 34, Sequence = 35, Title = "Acids & Bases Part 2 (CAPS)", Description = "This lesson explores the various formats of acid-base/neutralisation reactions with multiple examples (including a recap on writing correct chemical formulae). We also cover a brief overview of titrations and work through various examples of acid-base calculations and questions.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[12], Tags[14]] },
            new Lesson() { Id = 35, Sequence = 36, Title = "Acids & Bases Part 1 (IEB)", Description = "Learn about the fundamental concepts in acid-base chemistry, including the Lowry- Brønsted theory, common acids and bases, conjugate acid-base pairs and amphoteric substances. This lesson also explains the differences between strong vs. weak acids and as well as strong vs. weak bases.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[20], Tags[23], Tags[11], Tags[14]] },
            new Lesson() { Id = 36, Sequence = 37, Title = "Ideal Gases (CAPS)", Description = "This lesson explains the concepts of the kinetic theory of gases including the differences between real gases and ideal gases. We also learn about Boyle's law (including the theory, experiments and calculations, with examples).", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[24], Tags[12], Tags[14]] },
            new Lesson() { Id = 37, Sequence = 38, Title = "Ions, Valency & Writing Molecular Formulae", Description = "This lesson covers the important and fundamental concepts of ions and valency. It also teaches an effective method (called the Cross-Over Method) for using the charges of ions to correctly determine the chemical formulae of compounds. These essential skills are required throughout chemistry regardless of grade or syllabus.", IsFree = true, LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[11], Tags[12], Tags[14], Tags[25], Tags[26], Tags[27], Tags[28], Tags[29]] },

            // 2025 Published lessons
            new Lesson() { Id = 38, Sequence = 20, Title = "Electromagnetic Induction (IEB)", Description = "This lesson covers the IEB topic of Electromagnetic Induction, which is part of the bigger section known as Electrodynamics. In this lesson we will cover how electricity is induced in a conductor when magnets and conductors move relative to each other. This principle will be explained using Faraday's Law of Electromagnetic Induction. We will also cover concepts such as magnetic flux, magnetic flux density, magnetic flux linkage and Lenz's Law. We will take a look at the various scenarios of inserting and removing magnetic poles into/out of solenoids (also using the Right Hand Solenoid Rule) and will finish off the lesson by working through a couple of exam-type questions.", LessonType = LessonType.Video,
                Tags = [Tags[0], Tags[11], Tags[14], Tags[19], Tags[30]] },
            new Lesson() { Id = 39, Sequence = 39, Title = "Organic Chemistry Part 1", Description = "In this Chemistry lesson we are introduced to the section of Organic Chemistry. We will cover the special properties of carbon, look at some important definitions, and look at the ways in which organic molecules are represented (e.g. structural formulae, condensed formulae, IUPAC names, etc.). We will also look at the general steps that will be followed for naming most organic molecules.", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[11], Tags[12], Tags[15], Tags[31], Tags[32], Tags[33], Tags[34], Tags[35], Tags[36]] },
            new Lesson() { Id = 40, Sequence = 40, Title = "Organic Chemistry Part 2", Description = "In this lesson we focus on two homologous series known as the alkanes and the haloalkanes. We will cover how to draw their structural formulae (including their condensed structural formulae) as well as how to correctly name them.  We will also learn the system for naming carbon branches (which are called alkyl groups).", LessonType = LessonType.Video,
                Tags = [Tags[1], Tags[11], Tags[12], Tags[15], Tags[31], Tags[32], Tags[34], Tags[35], Tags[36], Tags[37], Tags[38]] },
        ];

        private static Video[] IntroVideos { get; } =
        [
            //Initial Lessons
            // Physics
            new Video() { Id = 1, Filename = $"{Lessons[0].Title}.mp4", Thumbnail = $"{Lessons[0].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[0] },
            new Video() { Id = 2, Filename = $"{Lessons[1].Title}.mp4", Thumbnail = $"{Lessons[1].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[1] },
            new Video() { Id = 3, Filename = $"{Lessons[2].Title}.mp4", Thumbnail = $"{Lessons[2].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[2] },
            new Video() { Id = 4, Filename = $"{Lessons[3].Title}.mp4", Thumbnail = $"{Lessons[3].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[3] },
            new Video() { Id = 5, Filename = $"{Lessons[4].Title}.mp4", Thumbnail = $"{Lessons[4].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[4] },
            new Video() { Id = 6, Filename = $"{Lessons[5].Title}.mp4", Thumbnail = $"{Lessons[5].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[5] },
            new Video() { Id = 7, Filename = $"{Lessons[6].Title}.mp4", Thumbnail = $"{Lessons[6].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[6] },
            new Video() { Id = 8, Filename = $"{Lessons[7].Title}.mp4", Thumbnail = $"{Lessons[7].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[7] },
            new Video() { Id = 9, Filename = $"{Lessons[8].Title}.mp4", Thumbnail = $"{Lessons[8].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[8] },
            new Video() { Id = 10, Filename = $"{Lessons[9].Title}.mp4", Thumbnail = $"{Lessons[9].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[9]  },
            new Video() { Id = 11, Filename = $"{Lessons[10].Title}.mp4", Thumbnail = $"{Lessons[10].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[10] },
            new Video() { Id = 12, Filename = $"{Lessons[11].Title}.mp4", Thumbnail = $"{Lessons[11].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[11] },
            new Video() { Id = 13, Filename = $"{Lessons[12].Title}.mp4", Thumbnail = $"{Lessons[12].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[12] },
            new Video() { Id = 14, Filename = $"{Lessons[13].Title}.mp4", Thumbnail = $"{Lessons[13].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[13] },
            new Video() { Id = 15, Filename = $"{Lessons[14].Title}.mp4", Thumbnail = $"{Lessons[14].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[14] },
            new Video() { Id = 16, Filename = $"{Lessons[15].Title}.mp4", Thumbnail = $"{Lessons[15].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[15] },
            new Video() { Id = 17, Filename = $"{Lessons[16].Title}.mp4", Thumbnail = $"{Lessons[16].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[16] },
            new Video() { Id = 18, Filename = $"{Lessons[17].Title}.mp4", Thumbnail = $"{Lessons[17].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[17] },
            new Video() { Id = 19, Filename = $"{Lessons[18].Title}.mp4", Thumbnail = $"{Lessons[18].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[18] },
            // Chemistry
            new Video() { Id = 20, Filename = $"{Lessons[19].Title}.mp4", Thumbnail = $"{Lessons[19].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[19] },
            new Video() { Id = 21, Filename = $"{Lessons[20].Title}.mp4", Thumbnail = $"{Lessons[20].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[20] },
            new Video() { Id = 22, Filename = $"{Lessons[21].Title}.mp4", Thumbnail = $"{Lessons[21].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[21] },
            new Video() { Id = 23, Filename = $"{Lessons[22].Title}.mp4", Thumbnail = $"{Lessons[22].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[22] },
            new Video() { Id = 24, Filename = $"{Lessons[23].Title}.mp4", Thumbnail = $"{Lessons[23].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[23] },
            new Video() { Id = 25, Filename = $"{Lessons[24].Title}.mp4", Thumbnail = $"{Lessons[24].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[24] },
            new Video() { Id = 26, Filename = $"{Lessons[25].Title}.mp4", Thumbnail = $"{Lessons[25].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[25] },
            new Video() { Id = 27, Filename = $"{Lessons[26].Title}.mp4", Thumbnail = $"{Lessons[26].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[26] },
            new Video() { Id = 28, Filename = $"{Lessons[27].Title}.mp4", Thumbnail = $"{Lessons[27].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[27] },
            new Video() { Id = 29, Filename = $"{Lessons[28].Title}.mp4", Thumbnail = $"{Lessons[28].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[28] },
            new Video() { Id = 30, Filename = $"{Lessons[29].Title}.mp4", Thumbnail = $"{Lessons[29].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[29] },
            new Video() { Id = 31, Filename = $"{Lessons[30].Title}.mp4", Thumbnail = $"{Lessons[30].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[30] },
            new Video() { Id = 32, Filename = $"{Lessons[31].Title}.mp4", Thumbnail = $"{Lessons[31].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[31] },
            new Video() { Id = 33, Filename = $"{Lessons[32].Title}.mp4", Thumbnail = $"{Lessons[32].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[32] },
            new Video() { Id = 34, Filename = $"{Lessons[33].Title}.mp4", Thumbnail = $"{Lessons[33].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[33] },
            new Video() { Id = 35, Filename = $"{Lessons[34].Title}.mp4", Thumbnail = $"{Lessons[34].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[34] },
            new Video() { Id = 36, Filename = $"{Lessons[35].Title}.mp4", Thumbnail = $"{Lessons[35].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[35] },
            new Video() { Id = 37, Filename = $"{Lessons[36].Title}.mp4", Thumbnail = $"{Lessons[36].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[36] },
            
            // 2025 Published lessons
            new Video() { Id = 75, Filename = $"{Lessons[37].Title}.mp4", Thumbnail = $"{Lessons[37].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[37] },
            new Video() { Id = 77, Filename = $"{Lessons[38].Title}.mp4", Thumbnail = $"{Lessons[38].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[38] },
            new Video() { Id = 79, Filename = $"{Lessons[39].Title}.mp4", Thumbnail = $"{Lessons[39].Title}.jpg", RelativePath = "LimitedAccess", VideoType = VideoType.Intro, Lesson = Lessons[39] },
        ];

        private static Video[] LessonVideos { get; } =
        [
            // Initial Lessons
            // Physics
            new Video() { Id = 38, Filename = $"{Lessons[0].Title}.mp4", Thumbnail = $"{Lessons[0].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[0], Duration = new TimeSpan(0, 41, 06) },
            new Video() { Id = 39, Filename = $"{Lessons[1].Title}.mp4", Thumbnail = $"{Lessons[1].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[1], Duration = new TimeSpan(0,32,41) },
            new Video() { Id = 40, Filename = $"{Lessons[2].Title}.mp4", Thumbnail = $"{Lessons[2].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[2], Duration = new TimeSpan(0, 41, 39) },
            new Video() { Id = 41, Filename = $"{Lessons[3].Title}.mp4", Thumbnail = $"{Lessons[3].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[3], Duration = new TimeSpan(0, 37, 24) },
            new Video() { Id = 42, Filename = $"{Lessons[4].Title}.mp4", Thumbnail = $"{Lessons[4].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[4], Duration = new TimeSpan(0, 19, 28) },
            new Video() { Id = 43, Filename = $"{Lessons[5].Title}.mp4", Thumbnail = $"{Lessons[5].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[5], Duration = new TimeSpan(0, 34, 06) },
            new Video() { Id = 44, Filename = $"{Lessons[6].Title}.mp4", Thumbnail = $"{Lessons[6].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[6], Duration = new TimeSpan(0, 46, 20) },
            new Video() { Id = 45, Filename = $"{Lessons[7].Title}.mp4", Thumbnail = $"{Lessons[7].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[7], Duration = new TimeSpan(0, 53, 01) },
            new Video() { Id = 46, Filename = $"{Lessons[8].Title}.mp4", Thumbnail = $"{Lessons[8].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[8], Duration = new TimeSpan(0, 56, 19) },
            new Video() { Id = 47, Filename = $"{Lessons[9].Title}.mp4", Thumbnail = $"{Lessons[9].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[9], Duration = new TimeSpan(0, 46, 56) },
            new Video() { Id = 48, Filename = $"{Lessons[10].Title}.mp4", Thumbnail = $"{Lessons[10].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[10], Duration = new TimeSpan(0, 22, 33) },
            new Video() { Id = 49, Filename = $"{Lessons[11].Title}.mp4", Thumbnail = $"{Lessons[11].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[11], Duration = new TimeSpan(1, 7, 56) },
            new Video() { Id = 50, Filename = $"{Lessons[12].Title}.mp4", Thumbnail = $"{Lessons[12].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[12], Duration = new TimeSpan(1, 9, 3) },
            new Video() { Id = 51, Filename = $"{Lessons[13].Title}.mp4", Thumbnail = $"{Lessons[13].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[13], Duration = new TimeSpan(1, 26, 16) },
            new Video() { Id = 52, Filename = $"{Lessons[14].Title}.mp4", Thumbnail = $"{Lessons[14].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[14], Duration = new TimeSpan(0, 45, 31) },
            new Video() { Id = 53, Filename = $"{Lessons[15].Title}.mp4", Thumbnail = $"{Lessons[15].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[15], Duration = new TimeSpan(0, 50, 16) },
            new Video() { Id = 54, Filename = $"{Lessons[16].Title}.mp4", Thumbnail = $"{Lessons[16].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[16], Duration = new TimeSpan(1, 8, 0) },
            new Video() { Id = 55, Filename = $"{Lessons[17].Title}.mp4", Thumbnail = $"{Lessons[17].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[17], Duration = new TimeSpan(1, 33, 57) },
            new Video() { Id = 56, Filename = $"{Lessons[18].Title}.mp4", Thumbnail = $"{Lessons[18].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[18], Duration = new TimeSpan(1, 09,46) },
            // Chemistry
            new Video() { Id = 57, Filename = $"{Lessons[19].Title}.mp4", Thumbnail = $"{Lessons[19].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[19], Duration = new TimeSpan(0, 22, 25) },
            new Video() { Id = 58, Filename = $"{Lessons[20].Title}.mp4", Thumbnail = $"{Lessons[20].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[20], Duration = new TimeSpan(0, 54, 14) },
            new Video() { Id = 59, Filename = $"{Lessons[21].Title}.mp4", Thumbnail = $"{Lessons[21].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[21], Duration = new TimeSpan(0, 33, 04) },
            new Video() { Id = 60, Filename = $"{Lessons[22].Title}.mp4", Thumbnail = $"{Lessons[22].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[22], Duration = new TimeSpan(0, 47, 04) },
            new Video() { Id = 61, Filename = $"{Lessons[23].Title}.mp4", Thumbnail = $"{Lessons[23].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[23], Duration = new TimeSpan(1, 16, 11) },
            new Video() { Id = 62, Filename = $"{Lessons[24].Title}.mp4", Thumbnail = $"{Lessons[24].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[24], Duration = new TimeSpan(0, 53, 14) },
            new Video() { Id = 63, Filename = $"{Lessons[25].Title}.mp4", Thumbnail = $"{Lessons[25].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[25], Duration = new TimeSpan(1, 17, 24) },
            new Video() { Id = 64, Filename = $"{Lessons[26].Title}.mp4", Thumbnail = $"{Lessons[26].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[26], Duration = new TimeSpan(0, 29, 26) },
            new Video() { Id = 65, Filename = $"{Lessons[27].Title}.mp4", Thumbnail = $"{Lessons[27].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[27], Duration = new TimeSpan(0, 9, 53) },
            new Video() { Id = 66, Filename = $"{Lessons[28].Title}.mp4", Thumbnail = $"{Lessons[28].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[28], Duration = new TimeSpan(1, 19, 34) },
            new Video() { Id = 67, Filename = $"{Lessons[29].Title}.mp4", Thumbnail = $"{Lessons[29].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[29], Duration = new TimeSpan(0, 45, 32) },
            new Video() { Id = 68, Filename = $"{Lessons[30].Title}.mp4", Thumbnail = $"{Lessons[30].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[30], Duration = new TimeSpan(0, 57, 55) },
            new Video() { Id = 69, Filename = $"{Lessons[31].Title}.mp4", Thumbnail = $"{Lessons[31].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[31], Duration = new TimeSpan(0, 55, 24) },
            new Video() { Id = 70, Filename = $"{Lessons[32].Title}.mp4", Thumbnail = $"{Lessons[32].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[32], Duration = new TimeSpan(0, 58, 47) },
            new Video() { Id = 71, Filename = $"{Lessons[33].Title}.mp4", Thumbnail = $"{Lessons[33].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[33], Duration = new TimeSpan(1, 7, 11) },
            new Video() { Id = 72, Filename = $"{Lessons[34].Title}.mp4", Thumbnail = $"{Lessons[34].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[34], Duration = new TimeSpan(1, 34, 14) },
            new Video() { Id = 73, Filename = $"{Lessons[35].Title}.mp4", Thumbnail = $"{Lessons[35].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[35], Duration = new TimeSpan(1, 34, 53) },
            new Video() { Id = 74, Filename = $"{Lessons[36].Title}.mp4", Thumbnail = $"{Lessons[36].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[36], Duration = new TimeSpan(1, 5, 24) },
            // 2025 Published lessons
            new Video() { Id = 76, Filename = $"{Lessons[37].Title}.mp4", Thumbnail = $"{Lessons[37].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[37], Duration = new TimeSpan(1,8,21) },
            new Video() { Id = 78, Filename = $"{Lessons[38].Title}.mp4", Thumbnail = $"{Lessons[38].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[38], Duration = new TimeSpan(0,51,39) },
            new Video() { Id = 80, Filename = $"{Lessons[39].Title}.mp4", Thumbnail = $"{Lessons[39].Title}.jpg", VideoType = VideoType.FullLesson, Lesson = Lessons[39], Duration = new TimeSpan(1,25,06) },
        ];

        private static Price[] Prices { get; } =
        [
            //Initial Prices
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

            //Launch Sale Prices
            // Physics
            new Price() { Id = 38, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[0] },
            new Price() { Id = 39, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[1] },
            new Price() { Id = 40, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[2] },
            new Price() { Id = 41, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[3] },
            new Price() { Id = 42, Amount = 60, PromoAmount = 54, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[4] },
            new Price() { Id = 43, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[5] },
            new Price() { Id = 44, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[6] },
            new Price() { Id = 45, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[7] },
            new Price() { Id = 46, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[8] },
            new Price() { Id = 47, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[9] },
            new Price() { Id = 48, Amount = 60, PromoAmount = 54, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[10] },
            new Price() { Id = 49, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[11] },
            new Price() { Id = 50, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[12] },
            new Price() { Id = 51, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[13] },
            new Price() { Id = 52, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[14] },
            new Price() { Id = 53, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[15] },
            new Price() { Id = 54, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[16] },
            new Price() { Id = 55, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[17] },
            new Price() { Id = 56, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[18] },
            // Chemistry
            new Price() { Id = 57, Amount = 60, PromoAmount = 54, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[19] },
            new Price() { Id = 58, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[20] },
            new Price() { Id = 59, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[21] },
            new Price() { Id = 60, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[22] },
            new Price() { Id = 61, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[23] },
            new Price() { Id = 62, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[24] },
            new Price() { Id = 63, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[25] },
            new Price() { Id = 64, Amount = 60, PromoAmount = 54, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[26] },
            new Price() { Id = 65, Amount = 60, PromoAmount = 54, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[27] },
            new Price() { Id = 66, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[28] },
            new Price() { Id = 67, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[29] },
            new Price() { Id = 68, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[30] },
            new Price() { Id = 69, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[31] },
            new Price() { Id = 70, Amount = 80, PromoAmount = 72, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[32] },
            new Price() { Id = 71, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[33] },
            new Price() { Id = 72, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[34] },
            new Price() { Id = 73, Amount = 100, PromoAmount = 90, EffectiveDate = new DateOnly(2024, 08, 01), Lesson = Lessons[35] },
            //new Price() { Id = 74, Amount = 0, Lesson = Lessons[36] },

            // 2025 Prices
            // Physics
            new Price() { Id = 74, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[0] },
            new Price() { Id = 75, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[1] },
            new Price() { Id = 76, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[2] },
            new Price() { Id = 77, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[3] },
            new Price() { Id = 78, Amount = 60, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[4] },
            new Price() { Id = 79, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[5] },
            new Price() { Id = 80, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[6] },
            new Price() { Id = 81, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[7] },
            new Price() { Id = 82, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[8] },
            new Price() { Id = 83, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[9] },
            new Price() { Id = 84, Amount = 60, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[10] },
            new Price() { Id = 85, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[11] },
            new Price() { Id = 86, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[12] },
            new Price() { Id = 87, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[13] },
            new Price() { Id = 88, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[14] },
            new Price() { Id = 89, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[15] },
            new Price() { Id = 90, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[16] },
            new Price() { Id = 91, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[17] },
            new Price() { Id = 92, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[18] },
            // Chemistry
            new Price() { Id = 93, Amount = 60, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[19] },
            new Price() { Id = 94, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[20] },
            new Price() { Id = 95, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[21] },
            new Price() { Id = 96, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[22] },
            new Price() { Id = 97, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[23] },
            new Price() { Id = 98, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[24] },
            new Price() { Id = 99, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[25] },
            new Price() { Id = 100, Amount = 60, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[26] },
            new Price() { Id = 101, Amount = 60, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[27] },
            new Price() { Id = 102, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[28] },
            new Price() { Id = 103, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[29] },
            new Price() { Id = 104, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[30] },
            new Price() { Id = 105, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[31] },
            new Price() { Id = 106, Amount = 80, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[32] },
            new Price() { Id = 107, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[33] },
            new Price() { Id = 108, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[34] },
            new Price() { Id = 109, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[35] },
            //new Price() { Id = 37, Amount = 0, Lesson = Lessons[36] },

            new Price() { Id = 110, Amount = 100, EffectiveDate = new DateOnly(2025, 01, 11), Lesson = Lessons[37] },

            // 2025 Permanently Reduced Prices
            // Physics
            new Price() { Id = 111, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[0] },
            new Price() { Id = 112, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[1] },
            new Price() { Id = 113, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[2] },
            new Price() { Id = 114, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[3] },
            new Price() { Id = 115, Amount = 54, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[4] },
            new Price() { Id = 116, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[5] },
            new Price() { Id = 117, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[6] },
            new Price() { Id = 118, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[7] },
            new Price() { Id = 119, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[8] },
            new Price() { Id = 120, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[9] },
            new Price() { Id = 121, Amount = 54, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[10] },
            new Price() { Id = 122, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[11] },
            new Price() { Id = 123, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[12] },
            new Price() { Id = 124, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[13] },
            new Price() { Id = 125, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[14] },
            new Price() { Id = 126, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[15] },
            new Price() { Id = 127, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[16] },
            new Price() { Id = 128, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[17] },
            new Price() { Id = 129, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[18] },
            // Chemistry
            new Price() { Id = 130, Amount = 54, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[19] },
            new Price() { Id = 131, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[20] },
            new Price() { Id = 132, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[21] },
            new Price() { Id = 133, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[22] },
            new Price() { Id = 134, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[23] },
            new Price() { Id = 135, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[24] },
            new Price() { Id = 136, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[25] },
            new Price() { Id = 137, Amount = 54, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[26] },
            new Price() { Id = 138, Amount = 54, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[27] },
            new Price() { Id = 139, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[28] },
            new Price() { Id = 140, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[29] },
            new Price() { Id = 141, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[30] },
            new Price() { Id = 142, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[31] },
            new Price() { Id = 143, Amount = 72, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[32] },
            new Price() { Id = 144, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[33] },
            new Price() { Id = 145, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[34] },
            new Price() { Id = 146, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[35] },
            //new Price() { Id = 37, Amount = 0, Lesson = Lessons[36] },

            new Price() { Id = 147, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[37] },
            new Price() { Id = 148, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[38] },
            new Price() { Id = 149, Amount = 90, EffectiveDate = new DateOnly(2025, 02, 17), Lesson = Lessons[39] }
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
            new Document() { Id = 25, DocumentType = DocumentType.ExercisePdf, Title = "Intermolecular Forces", Filename = "Intermolecular Forces.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[25]] },
            new Document() { Id = 26, DocumentType = DocumentType.Reference, Title = "Common Polyatomic Ions", Filename = "Common Polyatomic Ions.pdf", RelativePath = "Reference", Lessons = [Lessons[36]] },

            new Document() { Id = 27, DocumentType = DocumentType.ExercisePdf, Title = "Acids and Bases (CAPS) Part 2 [Draft]", Filename = "Acids and Bases (CAPS) Part 2.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[33]] },
            new Document() { Id = 28, DocumentType = DocumentType.ExercisePdf, Title = "Chemical Bonding and Intermolecular Forces [Draft]", Filename = "Chemical Bonding and Intermolecular Forces.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[26], Lessons[27]] },
            new Document() { Id = 29, DocumentType = DocumentType.ExercisePdf, Title = "Electromagnetism (CAPS) [Draft]", Filename = "Electromagnetism (CAPS).pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[17]] },
            new Document() { Id = 30, DocumentType = DocumentType.ExercisePdf, Title = "Energy and Chemical Change [Draft]", Filename = "Energy and Chemical Change.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[28]] },
            new Document() { Id = 31, DocumentType = DocumentType.ExercisePdf, Title = "Ideal Gases [Draft]", Filename = "Ideal Gases.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[35]] },
            new Document() { Id = 32, DocumentType = DocumentType.ExercisePdf, Title = "IEB Acids and Bases (Part 1) [Draft]", Filename = "IEB Acids and Bases (Part 1).pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[34]] },
            new Document() { Id = 33, DocumentType = DocumentType.ExercisePdf, Title = "IEB Electromagnetism [Draft]", Filename = "IEB Electromagnetism.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[18]] },
            new Document() { Id = 34, DocumentType = DocumentType.ExercisePdf, Title = "Redox Reactions (CAPS) Part 1 [Draft]", Filename = "Redox Reactions (CAPS) Part 1.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[29]] },
            new Document() { Id = 35, DocumentType = DocumentType.ExercisePdf, Title = "Redox Reactions (CAPS) Part 2 [Draft]", Filename = "Redox Reactions (CAPS) Part 2.pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[30]] },
            new Document() { Id = 36, DocumentType = DocumentType.ExercisePdf, Title = "Redox Reactions (IEB) [Draft]", Filename = "Redox Reactions (IEB).pdf", RelativePath = "ExercisePdfs", Lessons = [Lessons[31]] },
        ];

        private static LearningPath[] LearningPaths { get; set; } = 
        [
            new LearningPath() { Id = 1, Name = "Physics (Paper 1)" },
            new LearningPath() { Id = 2, Name = "Chemistry (Paper 2)"},
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
                await SyncEntityAsync(Tags, include: $"{nameof(Tag.Category)}", SyncTagCategories);
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
            target ??= source;

            // Prices
            var prices = Prices.Where(price => price.Lesson.Id == target.Id);
            target.Prices.Clear();
            foreach (var price in prices)
            {
                target.Prices.Add(new Price() { Amount = price.Amount, PromoAmount = price.PromoAmount, EffectiveDate = price.EffectiveDate, Lesson = target });
            }

            // Tags
            var existingTags = await dataContext.Tags.ToListAsync();
            var sourceTags = source.Tags.ToArray();
            target.Tags.Clear();
            foreach (var tag in sourceTags)
            {
                var existingTag = existingTags.Single(x => x.Id == tag.Id);
                if (!target.Tags.Contains(existingTag))
                {
                    target.Tags.Add(existingTag);
                }
            }

            //Documents
            target.Documents.Clear();
        }

        private async Task SyncVideoLessonsAsync(Video source, Video? target)
        {
            target ??= source;

            var existingLessons = await dataContext.Lessons.ToListAsync();
            target.Lesson = existingLessons.Single(x => x.Id == source.Lesson.Id);
        }

        private async Task SyncDocumentLessonsAsync(Document source, Document? target)
        {
            target ??= source;
            var existingLessons = await dataContext.Lessons.ToListAsync();

            var sourceLessons = source.Lessons.ToArray();
            target.Lessons.Clear();
            foreach (var lesson in sourceLessons)
            {
                target.Lessons.Add(existingLessons.Single(existingLesson => existingLesson.Id == lesson.Id));
            }


        }
        private async Task SyncTagCategories(Tag source, Tag? target)
        {
            if (target != null)
                return;

            var existingTagCategories = await dataContext.TagCategories.ToListAsync();
            source.Category = existingTagCategories.Single(x => x.Id == source.Category.Id);
        }
    }
}
