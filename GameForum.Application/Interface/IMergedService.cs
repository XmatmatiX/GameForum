using GameForum.Application.ViewModels.MergedVm;
using GameForum.Application.ViewModels.Paragraphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Interface
{
    public interface IMergedService
    {
        NewPostWithGenresVm GetNewPostTemplate();
        int AddNewPost(NewPostWithGenresVm newPost);
        void AddNewParagraph(NewParagraphVm newParagraph);
        
    }
}
