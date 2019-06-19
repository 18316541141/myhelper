using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    public enum FileTypeEnum:long
    {
        JPG = 0xFFD8FF,
        GIF= 0x47494638,
        TIF= 0x49492A00,
        BMP= 0x424D,
        DWG= 0x41433130,
        PSD= 0x38425053,
        RTF= 0x7B5C727466,
        XML= 0x3C3F786D6C,
        HTML= 0x68746D6C3E,
        DOC= 0xD0CF11E0,
        XLS= 0xD0CF11E0,
        PDF= 0x255044462D312E,
        DOCX= 0x504B0304,
        XLSX= 0x504B0304,
        RAR= 0x52617221,
        WAV= 0x57415645,
        AVI= 0x41564920,
        GZ= 0x1F8B08,
        OTHER=-1
    }
}
