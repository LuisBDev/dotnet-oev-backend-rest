using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers
{
    public class UserLessonProgressMapper
    {
        public static UserLessonProgressResponseDTO EntityToResponseDTO(UserLessonProgress userLessonProgress)
        {
            if (userLessonProgress == null)
                throw new ArgumentNullException(nameof(userLessonProgress));

            return new UserLessonProgressResponseDTO
            {
                Id = userLessonProgress.Id,
                UserId = userLessonProgress.User?.Id ?? 0,
                LessonId = userLessonProgress.Lesson?.Id ?? 0,
                LessonTitle = userLessonProgress.Lesson?.Title ?? string.Empty,
                LessonVideoKey = userLessonProgress.Lesson?.VideoKey ?? string.Empty,
                Duration = userLessonProgress.Lesson?.Duration ?? 0,
                Status = userLessonProgress.Status,
                CompletedAt = userLessonProgress.CompletedAt
            };
        }

        public static List<UserLessonProgressResponseDTO> EntityToResponseDTO(List<UserLessonProgress> userLessonProgresses)
        {
            if (userLessonProgresses == null)
                return new List<UserLessonProgressResponseDTO>();

            return userLessonProgresses
                .Select(EntityToResponseDTO)
                .ToList();
        }
    }
}