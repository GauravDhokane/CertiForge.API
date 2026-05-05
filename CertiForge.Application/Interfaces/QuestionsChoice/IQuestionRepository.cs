using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertiForge.Application.DTOs;
using CertiForge.Domain.Entities;

namespace CertiForge.Application.Interfaces.QuestionsChoice
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<Question> AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(Question question);
        Task UpdateQuestionAndChoicesAsync(int id, QuestionDto dto);
    }
}
