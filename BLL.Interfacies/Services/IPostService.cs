using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IPostService
    {
        PostEntity GetPostEntity(int id);
        IEnumerable<PostEntity> GetAllPostEntities();
        void CreatePost( PostEntity post);
        void DeletePost(PostEntity post);
        void Update(PostEntity post);
    }
}
