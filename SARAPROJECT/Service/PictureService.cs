namespace SARAPROJECT.Service
{
    public class PictureService
    {
        public PictureService()
        {

        }
     
        public String Insert(IFormFile FOTOPRODUCTO )
        {
            Guid g = Guid.NewGuid();
            String imgName = g.ToString().Substring(0, 10) + FOTOPRODUCTO.FileName.Substring(FOTOPRODUCTO.FileName.Length - 5, 5);
            String path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Content/imgProducts/", imgName);
            using var Stream = new FileStream(path, FileMode.Create);
            FOTOPRODUCTO.CopyToAsync(Stream);
            return imgName;
        }

        public void Delete(String ImgName)
        {
            var foto = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Content/imgProducts/",ImgName);
            if (System.IO.File.Exists(foto))
                System.IO.File.Delete(foto);
        }

    }
}
