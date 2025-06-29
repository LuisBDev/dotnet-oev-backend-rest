using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    // private readonly IS3Service _s3Service; 

    public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        // _s3Service = s3Service;
    }

    public async Task<IReadOnlyList<CourseResponseDTO>> FindAllCoursesAsync()
    {
        var courses = await _unitOfWork.CourseRepository.FindAllCoursesIncludingAuthorAsync();
        return _mapper.Map<IReadOnlyList<CourseResponseDTO>>(courses);
    }

// Método asíncrono que devuelve una lista de cursos (DTOs) publicados por un usuario específico.
    public async Task<IReadOnlyList<CourseResponseDTO>> FindAllCoursesByUserIdAsync(long userId)
    {
        // Llama al repositorio para obtener todos los cursos publicados por el usuario con el ID especificado.
        var courses = await _unitOfWork.CourseRepository.FindCoursesPublishedByUserIdAsync(userId);

        // Utiliza AutoMapper (_mapper) para convertir la lista de entidades Course a una lista de CourseResponseDTO.
        var courseResponseDtoList = _mapper.Map<IReadOnlyList<CourseResponseDTO>>(courses);

        // Retorna la lista de cursos en formato DTO, lista para ser usada en la respuesta de una API, por ejemplo.
        return courseResponseDtoList;
    }


    public async Task<CourseResponseDTO?> FindCourseByIdAsync(long id)
    {
        // Usamos el método que carga al autor para que el mapeo sea completo
        var course = await _unitOfWork.CourseRepository.FindCourseWithAuthorByIdAsync(id);
        if (course == null) throw new NotFoundException($"Course with id {id} not found");
        return _mapper.Map<CourseResponseDTO>(course);
    }

    public async Task<CourseResponseDTO> CreateCourseAsync(long userId, CourseRequestDTO courseRequestDTO)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
        if (user == null) throw new NotFoundException($"User with id {userId} not found");

        if (user.Role != Role.Instructor) throw new ForbiddenException($"User with id {userId} is not allowed to create courses");

        var course = _mapper.Map<Course>(courseRequestDTO);
        course.UserId = userId; // Asignamos la clave foránea
        course.CreationDate = DateTime.UtcNow;
        course.TotalStudents = 0;
        course.TotalLessons = 0;
        course.Status = "ACTIVE";

        await _unitOfWork.CourseRepository.AddAsync(course);
        await _unitOfWork.CompleteAsync(); // Guardamos en la base de datos

        course.User = user; // Asignamos el objeto para el mapeo de respuesta

        // TODO: validar que el usuario se está asociando correctamente al curso

        return _mapper.Map<CourseResponseDTO>(course);
    }

    public async Task<CourseResponseDTO> UpdateCourseByIdAsync(long id, UpdateCourseRequestDTO updateDto)
    {
        var courseToUpdate = await _unitOfWork.CourseRepository.FindCourseWithAuthorByIdAsync(id);
        if (courseToUpdate == null) throw new NotFoundException($"Course with id {id} not found");

        // AutoMapper aplica los cambios del DTO a la entidad existente, ignorando nulos
        _mapper.Map(updateDto, courseToUpdate);
        courseToUpdate.LastUpdate = DateTime.UtcNow;

        // No es necesario llamar a Update() porque EF Core ya rastrea la entidad
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<CourseResponseDTO>(courseToUpdate);
    }

    public async Task<bool> DeleteCourseByIdAsync(long id)
    {
        // Obtenemos el curso y sus lecciones para poder borrar los videos
        var courseToDelete = await _unitOfWork.CourseRepository.FindCourseWithLessonsByIdAsync(id);
        if (courseToDelete == null)
            // En lugar de lanzar una excepción, podemos devolver false
            // para que el controlador devuelva un 404 Not Found.
            return false;

        if (courseToDelete.LessonList != null && courseToDelete.LessonList.Any())
            foreach (var lesson in courseToDelete.LessonList)
                if (!string.IsNullOrEmpty(lesson.VideoKey))
                {
                    // await _s3Service.DeleteFileAsync("oev-mooc-bucket", lesson.VideoKey);
                }

        _unitOfWork.CourseRepository.Delete(courseToDelete);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}