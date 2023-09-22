using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Amicitia.IO.Binary;
using AtlusScriptLibrary.Common.Text.Encodings;

namespace P5RStringEditor
{
    // Original Code from DeathChaos25's P5FTDStringConverter
    public class FTDStringConverter
    {
        public class FTD
        {
            public string Name { get; set; } = "";
            public List<FTDString> Lines { get; set; } = new List<FTDString>();
        }

        public class FTDString
        {
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "";
        }

        public static FTD ReadFTD(string ftdPath)
        {
            List<UInt32> StringPointers = new List<UInt32>();

            FTD ftd = new FTD() { Name = Path.GetFileName(ftdPath) };

            using (BinaryObjectReader ftdfile = new BinaryObjectReader(ftdPath, Endianness.Big, AtlusEncoding.Persona5RoyalEFIGS))
            {
                var temp1 = ftdfile.ReadUInt16(); // should be 00 01
                var temp2 = ftdfile.ReadUInt16(); // should be 00 00
                UInt32 Magic = ftdfile.ReadUInt32(); // FTD0
                UInt32 Filesize = ftdfile.ReadUInt32(); // location is 0x8
                var temp3 = ftdfile.ReadUInt16(); // should be 00 01
                var numOfPointers = ftdfile.ReadUInt16(); // location 0xE

                List<String> FTDStrings = new List<String>();

                for (int i = 0; i < numOfPointers; i++)
                {
                    StringPointers.Add(ftdfile.ReadUInt32());
                }

                for (int i = 0; i < numOfPointers; i++)
                {
                    ftdfile.Seek(StringPointers[i], SeekOrigin.Begin);
                    var stringLength = ftdfile.ReadByte();
                    var unk = ftdfile.ReadByte(); // should be 0x1
                    var nullVar = ftdfile.ReadUInt16(); // should be 00 00

                    ftd.Lines.Add(new FTDString() { Name = ftdfile.ReadString(StringBinaryFormat.NullTerminated), Id = i });
                }
            }

            return ftd;
        }

        public static void WriteFTD(FTD ftd, string savePath)
        {
            using (BinaryObjectWriter ftdfile = new BinaryObjectWriter(savePath, Endianness.Big, AtlusEncoding.Persona5RoyalEFIGS))
            {
                ftdfile.WriteUInt16(0x0001);
                ftdfile.WriteUInt16(0x0000);
                ftdfile.WriteUInt32(0x46544430); // FTD0
                ftdfile.WriteUInt32(0x0); // Filesize, come back and fix later
                ftdfile.WriteUInt16(0x0001);
                ftdfile.WriteUInt16(Convert.ToUInt16(ftd.Lines.Count));

                foreach (var line in ftd.Lines)
                {
                    ftdfile.WriteUInt32(0x0); //Write dummy pointers
                }

                int targetPadding = (int)((0x10 - ftdfile.Position % 0x10) % 0x10); // pad to end of line if not enough pointers
                if (targetPadding > 0)
                {
                    for (int j = 0; j < targetPadding; j++)
                    {
                        ftdfile.WriteByte((byte)0);
                    }
                }

                long NextPos = ftdfile.Position;
                int i = 0;
                foreach (var ftdString in ftd.Lines)
                {
                    long targetPointerPos = 0x10 + 4 * i;
                    ftdfile.Seek(targetPointerPos, SeekOrigin.Begin);
                    ftdfile.WriteUInt32((UInt32)NextPos);

                    ftdfile.Seek(NextPos, SeekOrigin.Begin);

                    var strLen = ftdString.Name.Length + 1;
                    ftdfile.WriteByte((byte)strLen);
                    ftdfile.WriteByte(1);
                    ftdfile.WriteUInt16(0);
                    ftdfile.WriteString(StringBinaryFormat.FixedLength, ftdString.Name, ftdString.Name.Length);
                    ftdfile.WriteByte(0);

                    targetPadding = (int)((0x10 - ftdfile.Position % 0x10) % 0x10);
                    if (targetPadding > 0)
                    {
                        for (int j = 0; j < targetPadding; j++)
                        {
                            ftdfile.WriteByte((byte)0);
                        }
                    }

                    NextPos = ftdfile.Position;
                    i++;
                }
                ftdfile.Seek(8, SeekOrigin.Begin);
                ftdfile.WriteUInt32((UInt32)ftdfile.Length); // fix filesize
                ftdfile.Dispose();
            }
        }
    }
}