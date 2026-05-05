using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.DTOs;

namespace CertiForge.Application.Interfaces.QuestionsChoice
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync();
        Task<QuestionDto?> GetQuestionByIdAsync(int id);
        Task AddQuestionAsync(CreateQuestionDto dto);
        Task UpdateQuestionAsync(int id, UpdateQuestionDto dto);
        Task DeleteQuestionAsync(int id);
        Task<QuestionDto> AddQuestionAndChoicesAsync(QuestionDto dto);
        Task UpdateQuestionAndChoicesAsync(int id, QuestionDto dto);
    }
}
