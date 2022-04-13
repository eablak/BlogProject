using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class Like 
    {
        //LİKE SINIFININ BASE MAP İLE İLİŞKİSİNİ KOPARDIK YANİ ORADAKİ İD,CREATDATE VB. PROPLARA ULAŞAMAYACAK , İSTERSEK YİNE BU SINIF İÇİNDE YAZABİLİRZ. VE CONFGURASYONUU DA YAPARKEN BASE DEN GELMEDİĞİ İÇİN IENTİTYTYPECONFİGURATİON DAN  KENDİ KALITIM ALDI VE CONFIGURE METOTUNU KENDİ TANIMLADI.  
        //navigation prop
        // 1 beğeni - 1 kullanıcıya aittir.

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        // 1 beğeni - 1 makaleye aittir.

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
