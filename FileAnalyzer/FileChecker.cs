using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Latch.FileAnalyzer
{
    public static class FileChecker
	{
		private static readonly Dictionary<string, List<FileSignature>> _fileSignatures = new() 
		{
			{
				".123", // application/vnd.lotus-1-2-3
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x05, 0x10, 0x04 } }
				}
			},
			{
				".cpl", // application/cpl+xml
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x5A } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xDC, 0xDC } }
				}
			},
			{
				".epub", // application/epub+zip
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x0A, 0x00, 0x02, 0x00 } }
				}
			},
			{
				".ttf", // application/font-sfnt
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".gz", // application/gzip
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1F, 0x8B, 0x08 } }
				}
			},
			{
				".tgz", // application/gzip
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1F, 0x8B, 0x08 } }
				}
			},
			{
				".hqx", // application/mac-binhex40
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x28, 0x54, 0x68, 0x69, 0x73, 0x20, 0x66, 0x69, 0x6C, 0x65, 0x20, 0x6D, 0x75, 0x73, 0x74, 0x20, 0x62, 0x65, 0x20, 0x63, 0x6F, 0x6E, 0x76, 0x65, 0x72, 0x74, 0x65, 0x64, 0x20, 0x77, 0x69, 0x74, 0x68, 0x20, 0x42, 0x69, 0x6E, 0x48, 0x65, 0x78, 0x20 } }
				}
			},
			{
				".doc", // application/msword
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x0D, 0x44, 0x4F, 0x43 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xDB, 0xA5, 0x2D, 0x00 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0xEC, 0xA5, 0xC1, 0x00 } }
				}
			},
			{
				".mxf", // application/mxf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x06, 0x0E, 0x2B, 0x34, 0x02, 0x05, 0x01, 0x01, 0x0D, 0x01, 0x02, 0x01, 0x01, 0x02 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C, 0x43, 0x54, 0x72, 0x61, 0x6E, 0x73, 0x54, 0x69, 0x6D, 0x65, 0x6C, 0x69, 0x6E, 0x65, 0x3E } }
				}
			},
			{
				".lha", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 2, Bytes = new byte[] { 0x2D, 0x6C, 0x68 } }
				}
			},
			{
				".lzh", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 2, Bytes = new byte[] { 0x2D, 0x6C, 0x68 } }
				}
			},
			{
				".exe", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x5A } }
				}
			},
			{
				".class", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xCA, 0xFE, 0xBA, 0xBE } }
				}
			},
			{
				".dll", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x5A } }
				}
			},
			{
				".img", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4A, 0x65, 0x74, 0x20, 0x44, 0x42 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x49, 0x43, 0x54, 0x00, 0x08 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x51, 0x46, 0x49, 0xFB } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x53, 0x43, 0x4D, 0x49 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x7E, 0x74, 0x2C, 0x01, 0x50, 0x70, 0x02, 0x4D, 0x52, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x31, 0x00, 0x00, 0x00, 0x31, 0x00, 0x00, 0x00, 0x43, 0x01, 0xFF, 0x00, 0x01, 0x00, 0x08, 0x00, 0x01, 0x00, 0x00, 0x00, 0x7e, 0x74, 0x2c, 0x01 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xEB, 0x3C, 0x90, 0x2A } }
				}
			},
			{
				".iso", // application/octet-stream
				new List<FileSignature>() {
					new FileSignature() { Offset = 32769, Bytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 } },
					new FileSignature() { Offset = 34817, Bytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 } },
					new FileSignature() { Offset = 36865, Bytes = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 } }
				}
			},
			{
				".ogx", // application/ogg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".oxps", // application/oxps
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".pdf", // application/pdf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x25, 0x50, 0x44, 0x46 } }
				}
			},
			{
				".p10", // application/pkcs10
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x64, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".pls", // application/pls+xml
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x5B, 0x70, 0x6C, 0x61, 0x79, 0x6C, 0x69, 0x73, 0x74, 0x5D } }
				}
			},
			{
				".eps", // application/postscript
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x25, 0x21, 0x50, 0x53, 0x2D, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x2D, 0x33, 0x2E, 0x30, 0x20, 0x45, 0x50, 0x53, 0x46, 0x2D, 0x33, 0x20, 0x30 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xC5, 0xD0, 0xD3, 0xC6 } }
				}
			},
			{
				".ai", // application/postscript
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x25, 0x50, 0x44, 0x46 } }
				}
			},
			{
				".rtf", // application/rtf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 } }
				}
			},
			{
				".tsa", // application/tamp-sequence-adjust
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x47 } }
				}
			},
			{
				".msf", // application/vnd.epson.msf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2F, 0x2F, 0x20, 0x3C, 0x21, 0x2D, 0x2D, 0x20, 0x3C, 0x6D, 0x64, 0x62, 0x3A, 0x6D, 0x6F, 0x72, 0x6B, 0x3A, 0x7A } }
				}
			},
			{
				".fdf", // application/vnd.fdf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x25, 0x50, 0x44, 0x46 } }
				}
			},
			{
				".fm", // application/vnd.framemaker
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C, 0x4D, 0x61, 0x6B, 0x65, 0x72, 0x46, 0x69, 0x6C, 0x65, 0x20 } }
				}
			},
			{
				".kmz", // application/vnd.google-earth.kmz
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".tpl", // application/vnd.groove-tool-template
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x20, 0xAF, 0x30 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x6D, 0x73, 0x46, 0x69, 0x6C, 0x74, 0x65, 0x72, 0x4C, 0x69, 0x73, 0x74 } }
				}
			},
			{
				".kwd", // application/vnd.kde.kword
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".wk4", // application/vnd.lotus-1-2-3
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x02, 0x10, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".wk3", // application/vnd.lotus-1-2-3
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x00, 0x10, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".wk1", // application/vnd.lotus-1-2-3
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x02, 0x00, 0x06, 0x04, 0x06, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".apr", // application/vnd.lotus-approach
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }
				}
			},
			{
				".nsf", // application/vnd.lotus-notes
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1A, 0x00, 0x00, 0x04, 0x00, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4E, 0x45, 0x53, 0x4D, 0x1A, 0x01 } }
				}
			},
			{
				".ntf", // application/vnd.lotus-notes
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1A, 0x00, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x30, 0x31, 0x4F, 0x52, 0x44, 0x4E, 0x41, 0x4E, 0x43, 0x45, 0x20, 0x53, 0x55, 0x52, 0x56, 0x45, 0x59, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4E, 0x49, 0x54, 0x46, 0x30 } }
				}
			},
			{
				".org", // application/vnd.lotus-organizer
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x41, 0x4F, 0x4C, 0x56, 0x4D, 0x31, 0x30, 0x30 } }
				}
			},
			{
				".lwp", // application/vnd.lotus-wordpro
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x57, 0x6F, 0x72, 0x64, 0x50, 0x72, 0x6F } }
				}
			},
			{
				".sam", // application/vnd.lotus-wordpro
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x5B, 0x50, 0x68, 0x6F, 0x6E, 0x65, 0x5D } }
				}
			},
			{
				".mif", // application/vnd.mif
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C, 0x4D, 0x61, 0x6B, 0x65, 0x72, 0x46, 0x69, 0x6C, 0x65, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20 } }
				}
			},
			{
				".xul", // application/vnd.mozilla.xul+xml
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x3F, 0x3E } }
				}
			},
			{
				".asf", // application/vnd.ms-asf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C } }
				}
			},
			{
				".cab", // application/vnd.ms-cab-compressed
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x53, 0x63, 0x28 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x53, 0x43, 0x46 } }
				}
			},
			{
				".xls", // application/vnd.ms-excel
				new List<FileSignature>() {
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x04 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x20, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".xla", // application/vnd.ms-excel
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }
				}
			},
			{
				".chm", // application/vnd.ms-htmlhelp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x54, 0x53, 0x46 } }
				}
			},
			{
				".ppt", // application/vnd.ms-powerpoint
				new List<FileSignature>() {
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0x00, 0x6E, 0x1E, 0xF0 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0x0F, 0x00, 0xE8, 0x03 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0xA0, 0x46, 0x1D, 0xF0 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x04 } }
				}
			},
			{
				".pps", // application/vnd.ms-powerpoint
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }
				}
			},
			{
				".wks", // application/vnd.ms-works
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x0E, 0x57, 0x4B, 0x53 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0x00, 0x02, 0x00, 0x04, 0x04, 0x05, 0x54, 0x02, 0x00 } }
				}
			},
			{
				".wpl", // application/vnd.ms-wpl
				new List<FileSignature>() {
					new FileSignature() { Offset = 84, Bytes = new byte[] { 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x20, 0x4D, 0x65, 0x64, 0x69, 0x61, 0x20, 0x50, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x20, 0x2D, 0x2D, 0x20 } }
				}
			},
			{
				".xps", // application/vnd.ms-xpsdocument
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".cif", // application/vnd.multiad.creator.cif
				new List<FileSignature>() {
					new FileSignature() { Offset = 2, Bytes = new byte[] { 0x5B, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E } }
				}
			},
			{
				".odp", // application/vnd.oasis.opendocument.presentation
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".odt", // application/vnd.oasis.opendocument.text
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".ott", // application/vnd.oasis.opendocument.text-template
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".pptx", // application/vnd.openxmlformats-officedocument.presentationml.presentation
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 } }
				}
			},
			{
				".xlsx", // application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 } }
				}
			},
			{
				".docx", // application/vnd.openxmlformats-officedocument.wordprocessingml.document
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 } }
				}
			},
			{
				".prc", // application/vnd.palm
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x42, 0x4F, 0x4F, 0x4B, 0x4D, 0x4F, 0x42, 0x49 } },
					new FileSignature() { Offset = 60, Bytes = new byte[] { 0x74, 0x42, 0x4D, 0x50, 0x4B, 0x6E, 0x57, 0x72 } }
				}
			},
			{
				".pdb", // application/vnd.palm
				new List<FileSignature>() {
					new FileSignature() { Offset = 11, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x2D, 0x57, 0x20, 0x50, 0x6F, 0x63, 0x6B, 0x65, 0x74, 0x20, 0x44, 0x69, 0x63, 0x74, 0x69 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x43, 0x2F, 0x43, 0x2B, 0x2B, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x73, 0x6D, 0x5F } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x73, 0x7A, 0x65, 0x7A } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xAC, 0xED, 0x00, 0x05, 0x73, 0x72, 0x00, 0x12, 0x62, 0x67, 0x62, 0x6C, 0x69, 0x74, 0x7A, 0x2E } }
				}
			},
			{
				".qxd", // application/vnd.Quark.QuarkXPress
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x4D, 0x4D, 0x58, 0x50, 0x52 } }
				}
			},
			{
				".rar", // application/vnd.rar
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 } }
				}
			},
			{
				".mmf", // application/vnd.smaf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x4D, 0x4D, 0x44, 0x00, 0x00 } }
				}
			},
			{
				".cap", // application/vnd.tcpdump.pcap
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x54, 0x53, 0x53 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x58, 0x43, 0x50, 0x00 } }
				}
			},
			{
				".dmp", // application/vnd.tcpdump.pcap
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x44, 0x4D, 0x50, 0x93, 0xA7 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x41, 0x47, 0x45, 0x44, 0x55, 0x36, 0x34 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x41, 0x47, 0x45, 0x44, 0x55, 0x4D, 0x50 } }
				}
			},
			{
				".wpd", // application/vnd.wordperfect
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0x57, 0x50, 0x43 } }
				}
			},
			{
				".xar", // application/vnd.xara
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x78, 0x61, 0x72, 0x21 } }
				}
			},
			{
				".spf", // application/vnd.yamaha.smaf-phrase
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x53, 0x50, 0x46, 0x49, 0x00 } }
				}
			},
			{
				".dtd", // application/xml-dtd
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x07, 0x64, 0x74, 0x32, 0x64, 0x64, 0x74, 0x64 } }
				}
			},
			{
				".zip", // application/zip
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x01, 0x00, 0x63, 0x00, 0x00, 0x00, 0x00, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x07, 0x08 } },
					new FileSignature() { Offset = 30, Bytes = new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 } },
					new FileSignature() { Offset = 526, Bytes = new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 } },
					new FileSignature() { Offset = 29152, Bytes = new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 } }
				}
			},
			{
				".amr", // audio/AMR
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x23, 0x21, 0x41, 0x4D, 0x52 } }
				}
			},
			{
				".au", // audio/basic
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2E, 0x73, 0x6E, 0x64 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x64, 0x6E, 0x73, 0x2E } }
				}
			},
			{
				".m4a", // audio/mp4
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20 } }
				}
			},
			{
				".mp3", // audio/mpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x44, 0x33 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xFB } }
				}
			},
			{
				".oga", // audio/ogg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".ogg", // audio/ogg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".qcp", // audio/qcelp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x49, 0x46, 0x46 } }
				}
			},
			{
				".koz", // audio/vnd.audikoz
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x44, 0x33, 0x03, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".bmp", // image/bmp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x42, 0x4D } }
				}
			},
			{
				".dib", // image/bmp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x42, 0x4D } }
				}
			},
			{
				".emf", // image/emf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x01, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".fits", // image/fits
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x53, 0x49, 0x4D, 0x50, 0x4C, 0x45, 0x20, 0x20, 0x3D, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x54 } }
				}
			},
			{
				".gif", // image/gif
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 } }
				}
			},
			{
				".jp2", // image/jp2
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A } }
				}
			},
			{
				".jpg", // image/jpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".jpeg", // image/jpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".jpe", // image/jpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".jfif", // image/jpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".png", // image/png
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } }
				}
			},
			{
				".tiff", // image/tiff
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x20, 0x49 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x49, 0x2A, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2A } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2B } }
				}
			},
			{
				".tif", // image/tiff
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x20, 0x49 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x49, 0x2A, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2A } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2B } }
				}
			},
			{
				".psd", // image/vnd.adobe.photoshop
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x38, 0x42, 0x50, 0x53 } }
				}
			},
			{
				".dwg", // image/vnd.dwg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x41, 0x43, 0x31, 0x30 } }
				}
			},
			{
				".ico", // image/vnd.microsoft.icon
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x01, 0x00 } }
				}
			},
			{
				".mdi", // image/vnd.ms-modi
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x45, 0x50 } }
				}
			},
			{
				".hdr", // image/vnd.radiance
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x23, 0x3F, 0x52, 0x41, 0x44, 0x49, 0x41, 0x4E, 0x43, 0x45, 0x0A } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x49, 0x53, 0x63, 0x28 } }
				}
			},
			{
				".pcx", // image/vnd.zbrush.pcx
				new List<FileSignature>() {
					new FileSignature() { Offset = 512, Bytes = new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 } }
				}
			},
			{
				".wmf", // image/wmf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x01, 0x00, 0x09, 0x00, 0x00, 0x03 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD7, 0xCD, 0xC6, 0x9A } }
				}
			},
			{
				".eml", // message/rfc822
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x46, 0x72, 0x6F, 0x6D, 0x3A, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x65, 0x74, 0x75, 0x72, 0x6E, 0x2D, 0x50, 0x61, 0x74, 0x68, 0x3A, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x58, 0x2D } }
				}
			},
			{
				".art", // message/rfc822
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4A, 0x47, 0x04, 0x0E } }
				}
			},
			{
				".manifest", // text/cache-manifest
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D } }
				}
			},
			{
				".log", // text/plain
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2A, 0x2A, 0x2A, 0x20, 0x20, 0x49, 0x6E, 0x73, 0x74, 0x61, 0x6C, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x53, 0x74, 0x61, 0x72, 0x74, 0x65, 0x64, 0x20 } }
				}
			},
			{
				".tsv", // text/tab-separated-values
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x47 } }
				}
			},
			{
				".vcf", // text/vcard
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x52, 0x44, 0x0D, 0x0A } }
				}
			},
			{
				".dms", // text/vnd.DMClientScript
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x44, 0x4D, 0x53, 0x21 } }
				}
			},
			{
				".dot", // text/vnd.graphviz
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }
				}
			},
			{
				".ts", // text/vnd.trolltech.linguist
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x47 } }
				}
			},
			{
				".3gp", // video/3gpp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 } }
				}
			},
			{
				".3g2", // video/3gpp2
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 } }
				}
			},
			{
				".mp4", // video/mp4
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70, 0x35 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x1C, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56, 0x01, 0x29, 0x00, 0x46, 0x4D, 0x53, 0x4E, 0x56, 0x6D, 0x70, 0x34, 0x32 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70, 0x35 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D } }
				}
			},
			{
				".m4v", // video/mp4
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 } }
				}
			},
			{
				".mpeg", // video/mpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x01, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".mpg", // video/mpeg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x01, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x01, 0xBA } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFF, 0xD8 } }
				}
			},
			{
				".ogv", // video/ogg
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".mov", // video/quicktime
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20 } },
					new FileSignature() { Offset = 4, Bytes = new byte[] { 0x6D, 0x6F, 0x6F, 0x76 } }
				}
			},
			{
				".cpt", // application/mac-compactpro
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x43, 0x50, 0x54, 0x37, 0x46, 0x49, 0x4C, 0x45 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x43, 0x50, 0x54, 0x46, 0x49, 0x4C, 0x45 } }
				}
			},
			{
				".sxc", // application/vnd.sun.xml.calc
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".sxd", // application/vnd.sun.xml.draw
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".sxi", // application/vnd.sun.xml.impress
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".sxw", // application/vnd.sun.xml.writer
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".bz2", // application/x-bzip2
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x42, 0x5A, 0x68 } }
				}
			},
			{
				".vcd", // application/x-cdlink
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x45, 0x4E, 0x54, 0x52, 0x59, 0x56, 0x43, 0x44, 0x02, 0x00, 0x00, 0x01, 0x02, 0x00, 0x18, 0x58 } }
				}
			},
			{
				".csh", // application/x-csh
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x63, 0x75, 0x73, 0x68, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00 } }
				}
			},
			{
				".spl", // application/x-futuresplash
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x01, 0x00 } }
				}
			},
			{
				".jar", // application/x-java-archive
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4A, 0x41, 0x52, 0x43, 0x53, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x08, 0x00, 0x08, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x5F, 0x27, 0xA8, 0x89 } }
				}
			},
			{
				".rpm", // application/x-rpm
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xED, 0xAB, 0xEE, 0xDB } }
				}
			},
			{
				".swf", // application/x-shockwave-flash
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x43, 0x57, 0x53 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x46, 0x57, 0x53 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x5A, 0x57, 0x53 } }
				}
			},
			{
				".sit", // application/x-stuffit
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x53, 0x49, 0x54, 0x21, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x53, 0x74, 0x75, 0x66, 0x66, 0x49, 0x74, 0x20, 0x28, 0x63, 0x29, 0x31, 0x39, 0x39, 0x37, 0x2D } }
				}
			},
			{
				".tar", // application/x-tar
				new List<FileSignature>() {
					new FileSignature() { Offset = 257, Bytes = new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 } }
				}
			},
			{
				".xpi", // application/x-xpinstall
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 } }
				}
			},
			{
				".xz", // application/x-xz
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0xFD, 0x37, 0x7A, 0x58, 0x5A, 0x00 } }
				}
			},
			{
				".mid", // audio/midi
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x54, 0x68, 0x64 } }
				}
			},
			{
				".midi", // audio/midi
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x4D, 0x54, 0x68, 0x64 } }
				}
			},
			{
				".aiff", // audio/x-aiff
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x46, 0x4F, 0x52, 0x4D, 0x00 } }
				}
			},
			{
				".flac", // audio/x-flac
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x66, 0x4C, 0x61, 0x43, 0x00, 0x00, 0x00, 0x22 } }
				}
			},
			{
				".wma", // audio/x-ms-wma
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C } }
				}
			},
			{
				".ram", // audio/x-pn-realaudio
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x72, 0x74, 0x73, 0x70, 0x3A, 0x2F, 0x2F } }
				}
			},
			{
				".rm", // audio/x-pn-realaudio
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2E, 0x52, 0x4D, 0x46 } }
				}
			},
			{
				".ra", // audio/x-realaudio
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2E, 0x52, 0x4D, 0x46, 0x00, 0x00, 0x00, 0x12, 0x00 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x2E, 0x72, 0x61, 0xFD, 0x00 } }
				}
			},
			{
				".wav", // audio/x-wav
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x49, 0x46, 0x46 } }
				}
			},
			{
				".webp", // image/webp
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x49, 0x46, 0x46 } }
				}
			},
			{
				".pgm", // image/x-portable-graymap
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x50, 0x35, 0x0A } }
				}
			},
			{
				".rgb", // image/x-rgb
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x01, 0xDA, 0x01, 0x01, 0x00, 0x03 } }
				}
			},
			{
				".webm", // video/webm
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1A, 0x45, 0xDF, 0xA3 } }
				}
			},
			{
				".flv", // video/x-flv
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20 } },
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x46, 0x4C, 0x56, 0x01 } }
				}
			},
			{
				".mkv", // video/x-matroska
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x1A, 0x45, 0xDF, 0xA3 } }
				}
			},
			{
				".asx", // video/x-ms-asf
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x3C } }
				}
			},
			{
				".wmv", // video/x-ms-wmv
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C } }
				}
			},
			{
				".avi", // video/x-msvideo
				new List<FileSignature>() {
					new FileSignature() { Offset = 0, Bytes = new byte[] { 0x52, 0x49, 0x46, 0x46 } }
				}
			}
		};

		/// <summary>
		/// Attempts to determines the type of file which was the <paramref name="buffer"/> 
		/// could have been read from.
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="extension"></param>
		/// <returns>A flag indicating is the extension exists and an <paramref name="extension"/> which
		/// tells the type of file. This <paramref name="extension"/>includes the period (.) in the file
		/// extension</returns>
		public static bool GetFileTypeExtension(byte[] buffer, out string extension)
		{
			bool result = GetFileTypeExtension(new MemoryStream(buffer), out string ext);
			extension = ext;
			return result;
		}

		/// <summary>
		/// Attempts to determines the type of file which was the <paramref name="filestream"/> 
		/// could have been read from.
		/// </summary>
		/// <param name="filestream"></param>
		/// <param name="extension"></param>
		/// <returns>A flag indicating is the extension exists and an <paramref name="extension"/> which
		/// tells the type of file. This <paramref name="extension"/>includes the period (.) in the file
		/// extension</returns>
		public static bool GetFileTypeExtension(Stream filestream, out string extension)
		{
			using (var reader = new BinaryReader(filestream))
			{
				foreach (var signature in _fileSignatures)
				{
					var signatures = signature.Value;

					// get the number of bytes from the stream by the lenght of the longest known byte for the extension
					var headerBytes = reader.ReadBytes(signatures.Select(x => x.Bytes).Max(m => m.Length));

					if (signatures.Any(signature => headerBytes.Take(signature.Bytes.Length).SequenceEqual(signature.Bytes)))
					{
						extension = signature.Key;
						return true;
					}
				}

				extension = string.Empty;
				return false;
			}
		}

		/// <summary>
		/// In order to validate a file, you have to pass the extension(s) of the file types you wish to check if the
		/// file matches. If the file stream matches the required extension, the method returns true, otherwise false.
		/// </summary>
		/// <param name="buffer">An array of bytes obtained by reading the raw bytes of the file</param>
		/// <param name="extensions">The file extensions to validate against. These extensions include the period(.)</param>
		/// <returns>A flag inciting if the <paramref name="buffer"/> is a valid file of any of the extensions passed.</returns>
		public static bool IsValidFileTypeExtension(byte[] buffer, params string[] extensions)
		{
			return IsValidFileTypeExtension(new MemoryStream(buffer), extensions);
		}

		/// <summary>
		/// In order to validate a file, you have to pass the extension(s) of the file types you wish to check if the
		/// file matches. If the file stream matches the required extension, the method returns true, otherwise false.
		/// </summary>
		/// <param name="filestream">A stream obtained by reading the raw bytes of the file</param>
		/// <param name="extensions">The file extensions to validate against. These extensions include the period(.)</param>
		/// <returns>A flag inciting if the <paramref name="filestream"/> is a valid file of any of the extensions passed.</returns>
		public static bool IsValidFileTypeExtension(Stream filestream, params string[] extensions)
		{
			using (var reader = new BinaryReader(filestream))
			{
				foreach (string ext in extensions)
				{
					if (!_fileSignatures.ContainsKey(ext.ToLower()))
						continue;

					var signatures = _fileSignatures[ext];

					// get the number of bytes from the stream by the length of the longest known byte for the extension
					var headerBytes = reader.ReadBytes(signatures.Select(x => x.Bytes).Max(m => m.Length));

					if (signatures.Any(signature => headerBytes.Take(signature.Bytes.Length).SequenceEqual(signature.Bytes)))
						return true;
				}
				return false;
			}
		}
	}
}
