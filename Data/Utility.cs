namespace ShopTP22.Data
{
    public static class Utility
    {
        public static byte[]? FormFileToBytes(IFormFile file)
        {
            byte[]? data = null;
            using (var stream = new MemoryStream())
            {
                if (file != null)
                {
                    file.CopyTo(stream);

                    data = stream.ToArray();
                }
            }

            return data;
        }


        public static string? BytesToImageSource(byte[]? imageData)
        {
            if (imageData == null)
                return null;

            return string.Format("data:image.gif;base64,{0}", Convert.ToBase64String(imageData));
        }
    }
}
