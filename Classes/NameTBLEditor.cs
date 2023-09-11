using Amicitia.IO.Binary;
using AtlusScriptLibrary.Common.Text.Encodings;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Endianness = Amicitia.IO.Binary.Endianness;
using StringBinaryFormat = Amicitia.IO.Binary.StringBinaryFormat;

namespace P5RStringEditor
{
    // Code by DeathChaos25

    public class NameTBLEditor
    {
        public const int tblNumber = 38; // number of Name TBL sections, 34 for P5, 38 for P5R

        public static List<NameTblSection> ReadNameTBL(string tblPath)
        {
            List<NameTblSection> TblSections = new List<NameTblSection>();

            using (BinaryObjectReader NAMETBLFile = new BinaryObjectReader(tblPath, Endianness.Big, AtlusEncoding.Persona5RoyalEFIGS))
            {
                for (int i = 0; i < tblNumber / 2; i++)
                {
                    List<String> NameTBLStrings = new List<String>();
                    List<UInt16> StringPointers = new List<UInt16>();

                    int filesize = NAMETBLFile.ReadInt32();

                    int numOfPointers = filesize / 2;

                    for (int j = 0; j < numOfPointers; j++)
                    {
                        StringPointers.Add(NAMETBLFile.ReadUInt16());
                    }

                    int targetPadding = (int)((0x10 - NAMETBLFile.Position % 0x10) % 0x10);
                    if (targetPadding > 0)
                    {
                        NAMETBLFile.Seek(targetPadding, SeekOrigin.Current);
                    }

                    long basePos = NAMETBLFile.Position;

                    for (int j = 0; j < numOfPointers; j++)
                    {
                        NAMETBLFile.Seek(basePos + StringPointers[j] + 4, SeekOrigin.Begin);

                        var TargetString = NAMETBLFile.ReadString(StringBinaryFormat.NullTerminated);

                        if ((byte)TargetString[TargetString.Length - 1] == 10)
                        {
                            TargetString = TargetString.Remove(TargetString.Length - 1, 1);
                        }
                        NameTBLStrings.Add(TargetString);
                    }

                    targetPadding = (int)((0x10 - NAMETBLFile.Position % 0x10) % 0x10);
                    if (targetPadding > 0)
                    {
                        NAMETBLFile.Seek(targetPadding, SeekOrigin.Current);
                    }

                    TblSections.Add(new NameTblSection() { 
                        Name = GetTBLDirName(tblNumber, i),
                        Lines = NameTBLStrings }
                    );
                }
            }

            return TblSections;
        }

        public static void SaveNameTBL(List<NameTblSection> tblSections, string outPath)
        {
            

            using (BinaryObjectWriter NAMETBLFile = new BinaryObjectWriter(outPath, Endianness.Big, AtlusEncoding.Persona5RoyalEFIGS))
            {
                for (int i = 0; i < tblSections.Count(); i++)
                {
                    List<long> StringPointers = new List<long>();

                    long fileSizePosition = NAMETBLFile.Position;
                    NAMETBLFile.WriteUInt32(0); // filesize

                    int numOfPointers = tblSections[i].Lines.Count;

                    long StringPointersLocation = NAMETBLFile.Position;
                    for (int j = 0; j < numOfPointers; j++)
                    {
                        NAMETBLFile.WriteUInt16(0); // write dummy pointers
                    }

                    uint filesize = (uint)(NAMETBLFile.Position - fileSizePosition) - 4;

                    int targetPadding = (int)((0x10 - NAMETBLFile.Position % 0x10) % 0x10);
                    if (targetPadding > 0)
                    {
                        for (int j = 0; j < targetPadding; j++)
                        {
                            NAMETBLFile.WriteByte((byte)0);
                        }
                    }

                    long basePos = NAMETBLFile.Position; // save position before strings

                    NAMETBLFile.Seek(fileSizePosition, SeekOrigin.Begin); // seek back to fix filesize
                    NAMETBLFile.WriteUInt32(filesize); // filesize
                    NAMETBLFile.Seek(basePos, SeekOrigin.Begin); // go back to where we were


                    fileSizePosition = NAMETBLFile.Position;

                    //Write Strings
                    NAMETBLFile.WriteUInt32(0); // filesize

                    for (int j = 0; j < numOfPointers; j++)
                    {
                        StringPointers.Add(NAMETBLFile.Position - (fileSizePosition + 4));
                        NAMETBLFile.WriteString(StringBinaryFormat.NullTerminated, tblSections[i].Lines[j]);
                    }
                    filesize = (uint)(NAMETBLFile.Position - fileSizePosition) - 4;

                    targetPadding = (int)((0x10 - NAMETBLFile.Position % 0x10) % 0x10);
                    if (targetPadding > 0)
                    {
                        for (int j = 0; j < targetPadding; j++)
                        {
                            NAMETBLFile.WriteByte((byte)0);
                        }
                    }

                    basePos = NAMETBLFile.Position; // save position before strings

                    NAMETBLFile.Seek(fileSizePosition, SeekOrigin.Begin); // seek back to fix filesize
                    NAMETBLFile.WriteUInt32(filesize); // filesize

                    NAMETBLFile.Seek(StringPointersLocation, SeekOrigin.Begin); // seek back to write Pointers
                    for (int j = 0; j < numOfPointers; j++)
                    {
                        NAMETBLFile.WriteUInt16((ushort)StringPointers[j]);
                    }

                    NAMETBLFile.Seek(basePos, SeekOrigin.Begin); // go back to where we were
                }
            }
        }

        static string pad_an_int(int N, int P)
        {
            // string used in Format() method
            string s = "{0:";
            for (int i = 0; i < P; i++)
            {
                s += "0";
            }
            s += "}";

            // use of string.Format() method
            string value = string.Format(s, N);

            // return output
            return value;
        }
        static string GetTBLDirName(int tblNumber, int index)
        {
            // Initialization of array
            string[] tblNames = new string[] { "Arcanas", "Skills", "Enemies", "Personas", "Accessories", "Protectors", "Consumables",
                                               "Key Items", "Materials", "Melee Weapons", "Battle Actions", "Outfits", "Skill Cards",
                                               "Party FirstNames", "Party LastNames", "Confidant Names", "Ranged Weapons", "17",
                                               "18", "19", "20" };

            string[] tblNamesR = new string[] { "Arcanas", "Skills", "Skills Again", "Enemies", "Personas", "Traits", "Accessories",
                                                "Protectors", "Consumables", "Key Items", "Materials", "Melee Weapons", "Battle Actions",
                                                "Outfits", "Skill Cards", "Party FirstNames", "Party LastNames", "Confidant Names",
                                                "Ranged Weapons", "39", "39", "39", "39" };

            if (tblNumber == 34)
            {
                return tblNames[index];
            }
            else return tblNamesR[index];
        }
    }

    public class NameTblSection
    {
        public string Name { get; set; } = "";
        public List<string> Lines { get; set; } = new List<string>();
    }
}
