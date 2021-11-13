using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Concrete.Common
{
    [Table("File", Schema = "Common")]
    public class File : Auditable
    {
        /// <summary>
        /// Display name of the file
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        ///  The name where the file is saved. It must be unique. Ex: Guid.jpg
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Source { get; set; }
        /// <summary>
        /// File Type ex: .jpg
        /// </summary>
        [Required]
        [Column(@"File_Type")]
        [StringLength(10)]
        public string FileType { get; set; }
        /// <summary>
        /// Whether to store the file in wwwroot
        /// </summary>
        [Required]
        [Column(@"Is_It_Private")]
        public bool IsItPrivate { get; set; }
        /// <summary>
        /// If IsItPrivate is true , the file path of the stored location
        /// </summary>
        [StringLength(200)]
        [Column(@"File_Path")]
        public string FilePath { get; set; }

        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
