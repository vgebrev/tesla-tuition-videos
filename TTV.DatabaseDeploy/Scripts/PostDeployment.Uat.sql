-- Make sure all paid content is pointing to the placeholder video and document
update [Video] set [Filename] = 'TTV-Lesson-Placeholder.mp4' where LessonId <> 37 and VideoTypeId = 1
update [Document] set [Filename] = 'Placeholder.pdf', RelativePath = '' where Id not in (22, 26)

select * from Video inner join Lesson on Video.LessonId = Lesson.Id
select * from Document 