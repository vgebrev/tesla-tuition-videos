UPDATE [Video] 
SET [Video].[Thumbnail] = [Lesson].[Title] + '.jpg'
FROM [Video]
INNED JOIN [Lesson] ON [Video].[LessonId] = [Lesson].[Id]