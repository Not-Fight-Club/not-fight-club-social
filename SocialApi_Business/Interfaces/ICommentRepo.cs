using SocialApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApi_Business.Interfaces
{
   public interface ICommentRepo
    {
        Task<List<ViewComment>> CommentListAsync();
        //Task<List<ViewComment>> SpecificCommentAsync();
    }
}
