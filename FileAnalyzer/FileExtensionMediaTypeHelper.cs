using System.Collections.Generic;
using Latch.FileAnalyzer.Enums;

namespace Latch.FileAnalyzer
{
    public static class FileExtensionMediaTypeHelper
    {
        public static readonly Dictionary<string, MediaType> MediaTypes = new()
        {
            #region image files
            // image files
            { ".art", MediaType.Image }, // image/x-jg
            { ".bm", MediaType.Image }, // image/bmp
            { ".bmp", MediaType.Image }, // image/bmp, image/x-windows-bmp
            { ".dwg", MediaType.Image }, // image/vnd.dwg, image/x-dwg
            { ".dxf", MediaType.Image }, // image/vnd.dwg, image/x-dwg
            { ".fif", MediaType.Image }, // image/fif
            { ".flo", MediaType.Image }, // image/florian
            { ".fpx", MediaType.Image }, // image/vnd.fpx
            { ".fpx", MediaType.Image }, // image/vnd.net-fpx
            { ".g3", MediaType.Image }, // image/g3fax
            { ".gif", MediaType.Image }, // image/gif
            { ".ico", MediaType.Image }, // image/x-icon
            { ".ief", MediaType.Image }, // image/ief
            { ".iefs", MediaType.Image }, // image/ief
            { ".jfif", MediaType.Image }, // image/jpeg, image/pjpeg
            { ".jfif-tbnl", MediaType.Image }, // image/jpeg
            { ".jpe", MediaType.Image }, // image/jpeg, image/pjpeg
            { ".jpeg", MediaType.Image }, // image/jpeg, image/pjpeg
            { ".jpg", MediaType.Image }, // image/jpeg, image/pjpeg
            { ".jps", MediaType.Image }, // image/x-jps
            { ".jut", MediaType.Image }, // image/jutvision
            { ".mcf", MediaType.Image }, // image/vasa
            { ".nap", MediaType.Image }, // image/naplps
            { ".naplps", MediaType.Image }, // image/naplps
            { ".nif", MediaType.Image }, // image/x-niff
            { ".niff", MediaType.Image }, // image/x-niff
            { ".pbm", MediaType.Image }, // image/x-portable-bitmap
            { ".pct", MediaType.Image }, // image/x-pict
            { ".pcx", MediaType.Image }, // image/x-pcx
            { ".pgm", MediaType.Image }, // image/x-portable-graymap, image/x-portable-greymap
            { ".pic", MediaType.Image }, // image/pict
            { ".pict", MediaType.Image }, // image/pict
            { ".pm", MediaType.Image }, // image/x-xpixmap
            { ".png", MediaType.Image }, // image/png
            { ".pnm", MediaType.Image }, // image/x-portable-anymap
            { ".ppm", MediaType.Image }, // image/x-portable-pixmap
            { ".qif", MediaType.Image }, // image/x-quicktime
            { ".qti", MediaType.Image }, // image/x-quicktime
            { ".qtif", MediaType.Image }, // image/x-quicktime
            { ".ras", MediaType.Image }, // image/cmu-raster, image/x-cmu-raster
            { ".rast", MediaType.Image }, // image/cmu-raster
            { ".rf", MediaType.Image }, // image/vnd.rn-realflash
            { ".rgb", MediaType.Image }, // image/x-rgb
            { ".rp", MediaType.Image }, // image/vnd.rn-realpix
            { ".svf", MediaType.Image }, // image/vnd.dwg, image/x-dwg
            { ".tif", MediaType.Image }, // image/tiff, image/x-tiff
            { ".tiff", MediaType.Image }, // image/tiff, image/x-tiff
            { ".turbot", MediaType.Image }, // image/florian
            { ".wbmp", MediaType.Image }, // image/vnd.wap.wbmp
            { ".xbm", MediaType.Image }, // image/x-xbitmap, image/x-xbm, image/xbm
            { ".xif", MediaType.Image }, // image/vnd.xiff
            { ".xpm", MediaType.Image }, // image/x-xpixmap, image/xpm
            { ".ax-pngrt", MediaType.Image }, // image/png
            { ".xwd", MediaType.Image }, // image/x-xwd, image/x-xwindowdump

            #endregion

            #region audio files
            // audio extensions
            { ".aif", MediaType.Audio }, // audio/x-aiff, audio/aiff
            { ".aifc", MediaType.Audio }, // audio/x-aiff
            { ".aiff", MediaType.Audio }, // audio/aiff, audio/x-aiff
            { ".au", MediaType.Audio }, // audio/basic, audio/x-au
            { ".m2a", MediaType.Audio }, // audio/mpeg
            { ".m3u", MediaType.Audio }, // audio/x-mpequrl
            { ".mid", MediaType.Audio }, // audio/midi, audio/x-mid, audio/x-midi
            { ".kar", MediaType.Audio }, // audio/midi
            { ".la", MediaType.Audio }, // audio/nspaudio, audio/x-nspaudio
            { ".lam", MediaType.Audio }, // audio/x-liveaudio
            { ".it", MediaType.Audio }, // audio/it
            { ".gsd", MediaType.Audio }, // audio/x-gsm
            { ".gsm", MediaType.Audio }, // audio/x-gsm
            { ".funk", MediaType.Audio }, // audio/make
            { ".jam", MediaType.Audio }, // audio/x-jam
            { ".lma", MediaType.Audio }, // audio/nspaudio, audio/x-nspaudio
            { ".midi", MediaType.Audio }, // audio/midi, audio/x-mid, audio/x-midi
            { ".mjf", MediaType.Audio }, // audio/x-vnd.audioexplosion.mjuicemediafile
            { ".mod", MediaType.Audio }, // audio/mod, audio/x-mod
            { ".qcp", MediaType.Audio }, // audio/vnd.qcelp
            { ".ra", MediaType.Audio }, // audio/x-pn-realaudio, audio/x-pn-realaudio-plugin, audio/x-realaudio
            { ".ram", MediaType.Audio }, // audio/x-pn-realaudio
            { ".pfunk", MediaType.Audio }, // audio/make, audio/make.my.funk
            { ".mpa", MediaType.Audio }, // audio/mpeg
            { ".mpg", MediaType.Audio }, // audio/mpeg
            { ".mp3", MediaType.Audio }, // audio/mpeg3, audio/x-mpeg-3
            { ".mp2", MediaType.Audio }, // audio/mpeg, audio/x-mpeg
            { ".mpga", MediaType.Audio }, // audio/mpeg
            { ".my", MediaType.Audio }, // audio/make
            { ".rm", MediaType.Audio }, // audio/x-pn-realaudio
            { ".rmi", MediaType.Audio }, // audio/mid
            { ".rmm", MediaType.Audio }, // audio/x-pn-realaudio
            { ".rmp", MediaType.Audio }, // audio/x-pn-realaudio, audio/x-pn-realaudio-plugin
            { ".s3m", MediaType.Audio }, // audio/s3m
            { ".rpm", MediaType.Audio }, // audio/x-pn-realaudio-plugin
            { ".voc", MediaType.Audio }, // audio/voc, audio/x-voc
            { ".vox", MediaType.Audio }, // audio/voxware
            { ".vqe", MediaType.Audio }, // audio/x-twinvq-plugin
            { ".vqf", MediaType.Audio }, // audio/x-twinvq
            { ".vql", MediaType.Audio }, // audio/x-twinvq-plugin
            { ".tsi", MediaType.Audio }, // audio/tsp-audio
            { ".snd", MediaType.Audio }, // audio/basic, audio/x-adpcm
            { ".sid", MediaType.Audio }, // audio/x-psid
            { ".tsp", MediaType.Audio }, // audio/tsplayer
            { ".wav", MediaType.Audio }, // audio/wav, audio/x-wav
            { ".xm", MediaType.Audio }, // audio/xm

            #endregion

            #region video files
            //video extensions
            { ".mp4", MediaType.Video }, // 
            { ".avi", MediaType.Video }, // 

            #endregion

            { ".pdf", MediaType.Pdf }, // 

            #region document files
            // word document extensions
            { ".doc", MediaType.Document }, // application/msword
            { ".dot", MediaType.Document }, // application/msword
            { ".w60", MediaType.Document }, // application/wordperfect6.0
            { ".w61", MediaType.Document }, // application/wordperfect6.1
            { ".w6w", MediaType.Document }, // application/msword
            { ".wiz", MediaType.Document }, // application/msword
            { ".word", MediaType.Document }, // application/msword
            { ".wp", MediaType.Document }, // application/wordperfect
            { ".wp5", MediaType.Document }, // application/wordperfect, application/wordperfect6.0
            { ".wp6", MediaType.Document }, // application/wordperfect
            { ".wpd", MediaType.Document }, // application/wordperfect
            { ".docx", MediaType.Document }, // 
            #endregion

            #region executable files
            { ".exe", MediaType.Application }, // 

            #endregion

            #region presentation files
            // presentation files
            { ".pot", MediaType.Presentation }, // application/mspowerpoint, application/vnd.ms-powerpoint
            { ".ppa", MediaType.Presentation }, // application/vnd.ms-powerpoint
            { ".pps", MediaType.Presentation }, // application/mspowerpoint, application/vnd.ms-powerpoint
            { ".ppt", MediaType.Presentation }, // application/mspowerpoint, application/powerpoint, application/vnd.ms-powerpoint, application/x-mspowerpoint
            { ".ppz", MediaType.Presentation }, // application/mspowerpoint
            { ".pwz", MediaType.Presentation }, // application/vnd.ms-powerpoint
            #endregion

            #region spreadsheet files
            //spreadsheet files
            { ".csv", MediaType.SpreadSheet }, // 
            { ".xl", MediaType.SpreadSheet }, // application/excel
            { ".xla", MediaType.SpreadSheet }, // application/excel, application/x-excel, application/x-msexcel
            { ".xlb", MediaType.SpreadSheet }, // application/vnd.ms-excel, application/x-excel
            { ".xlc", MediaType.SpreadSheet }, // application/excel, application/vnd.ms-excel, application/x-excel
            { ".xld", MediaType.SpreadSheet }, // application/excel, application/x-excel
            { ".xlk", MediaType.SpreadSheet }, // application/excel, application/x-excel
            { ".xll", MediaType.SpreadSheet }, // application/vnd.ms-excel, application/x-excel
            { ".xlm", MediaType.SpreadSheet }, // application/excel, application/vnd.ms-excel, application/x-excel
            { ".xls", MediaType.SpreadSheet }, // application/excel, application/vnd.ms-excel, application/x-excel, application/x-msexcel
            { ".xlt", MediaType.SpreadSheet }, // application/excel, application/x-excel
            { ".xlv", MediaType.SpreadSheet }, // application/excel, application/x-excel
            { ".xlw", MediaType.SpreadSheet }, // application/excel, application/vnd.ms-excel, application/x-excel. application/x-msexcel
            { ".xlsx", MediaType.SpreadSheet }, // 
            #endregion

            #region plain text files
            // plain text files
            { ".abc", MediaType.Text }, // text/vnd.abc
            { ".acgi", MediaType.Text }, // text/html
            { ".aip", MediaType.Text }, // text/x-audiosoft-intra
            { ".asm", MediaType.Text }, // text/x-asm
            { ".asp", MediaType.Text }, // text/asp
            { ".c", MediaType.Text }, // text/plain, text/x-c
            { ".c++", MediaType.Text }, // text/plain
            { ".cc", MediaType.Text }, // text/plain, text/x-c
            { ".com", MediaType.Text }, // text/plain
            { ".conf", MediaType.Text }, // text/plain
            { ".cpp", MediaType.Text }, // text/x-c
            { ".csh", MediaType.Text }, // text/x-script.csh
            { ".css", MediaType.Text }, // text/css
            { ".cxx", MediaType.Text }, // text/plain
            { ".def", MediaType.Text }, // text/plain
            { ".el", MediaType.Text }, // text/x-script.elisp
            { ".etx", MediaType.Text }, // text/x-setext
            { ".f", MediaType.Text }, // text/plain, text/x-fortran
            { ".f77", MediaType.Text }, // text/x-fortran
            { ".f90", MediaType.Text }, // text/plain, text/x-fortran
            { ".flx", MediaType.Text }, // text/vnd.fmi.flexstor
            { ".for", MediaType.Text }, // text/plain, text/x-fortran
            { ".g", MediaType.Text }, // text/plain
            { ".h", MediaType.Text }, // text/plain, text/x-h
            { ".hh", MediaType.Text }, // text/plain, text/x-h 
            { ".hlb", MediaType.Text }, // text/x-script
            { ".htc", MediaType.Text }, // text/x-component
            { ".htm", MediaType.Text }, // text/html
            { ".html", MediaType.Text }, // text/html
            { ".htmls", MediaType.Text }, // text/html
            { ".htt", MediaType.Text }, // text/webviewhtml
            { ".htx", MediaType.Text }, // text/html
            { ".idc", MediaType.Text }, // text/plain
            { ".jav", MediaType.Text }, // text/plain, text/x-java-source
            { ".java", MediaType.Text }, // text/plain, text/x-java-source
            { ".js", MediaType.Text }, // text/javascript, text/ecmascript
            { ".ksh", MediaType.Text }, // text/x-script.ksh
            { ".list", MediaType.Text }, // text/plain
            { ".log", MediaType.Text }, // text/plain
            { ".lsp", MediaType.Text }, // text/x-script.lisp
            { ".lst", MediaType.Text }, // text/plain
            { ".lsx", MediaType.Text }, // text/x-la-asf
            { ".m", MediaType.Text }, // text/plain, text/x-m
            { ".mar", MediaType.Text }, // text/plain
            { ".mcf", MediaType.Text }, // text/mcf
            { ".p", MediaType.Text }, // text/x-pascal
            { ".pas", MediaType.Text }, // text/pascal
            { ".pl", MediaType.Text }, // text/plain, text/x-script.perl
            { ".pm", MediaType.Text }, // text/x-script.perl-module
            { ".py", MediaType.Text }, // text/x-script.phyton
            { ".rexx", MediaType.Text }, // text/x-script.rexx
            { ".rt", MediaType.Text }, // text/richtext, text/vnd.rn-realtext
            { ".rtf", MediaType.Text }, // text/richtext
            { ".rtx", MediaType.Text }, // text/richtext
            { ".s", MediaType.Text }, // text/x-asm
            { ".scm", MediaType.Text }, // text/x-script.guile, text/x-script.scheme
            { ".sdml", MediaType.Text }, // text/plain
            { ".sgm", MediaType.Text }, // text/sgml, text/x-sgml, text/sgml, text/x-sgml
            { ".sh", MediaType.Text }, // text/x-script.sh
            { ".shtml", MediaType.Text }, // text/html
            { ".shtml", MediaType.Text }, // text/x-server-parsed-html
            { ".spc", MediaType.Text }, // text/x-speech
            { ".ssi", MediaType.Text }, // text/x-server-parsed-html
            { ".talk", MediaType.Text }, // text/x-speech
            { ".tcl", MediaType.Text }, // text/x-script.tcl
            { ".tcsh", MediaType.Text }, // text/x-script.tcsh
            { ".text", MediaType.Text }, // text/plain
            { ".tsv", MediaType.Text }, // text/tab-separated-values
            { ".txt", MediaType.Text }, // text/plain
            { ".uil", MediaType.Text }, // text/x-uil
            { ".uni", MediaType.Text }, // text/uri-list
            { ".unis", MediaType.Text }, // text/uri-list
            { ".uri", MediaType.Text }, // text/uri-list
            { ".uris", MediaType.Text }, // text/uri-list
            { ".uu", MediaType.Text }, // text/x-uuencode
            { ".uue", MediaType.Text }, // text/x-uuencode
            { ".vcs", MediaType.Text }, // text/x-vcalendar
            { ".wml", MediaType.Text }, // text/vnd.wap.wml
            { ".wmls", MediaType.Text }, // text/vnd.wap.wmlscript
            { ".wsc", MediaType.Text }, // text/scriplet
            { ".xml", MediaType.Text }, // text/xml
            { ".zsh", MediaType.Text }, // text/x-script.zsh

            #endregion
        
        };
    }
}
