using DoVuiHaiNao.Models.GalleryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface IGalleryRepository
    {
        Task<GalleryViewModel> GetAll(int? page, int? pageSize);
    }
}
