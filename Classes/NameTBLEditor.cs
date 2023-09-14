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

        public static List<TblSection> ReadNameTBL(string tblPath)
        {
            List<TblSection> TblSections = new List<TblSection>();

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

                    var tblSection = new TblSection() { SectionName = GetTBLDirName(tblNumber, i) };

                    for (int x = 0; x < NameTBLStrings.Count; x++)
                        tblSection.TblEntries.Add(new Entry() { ItemName = NameTBLStrings[x], OldName = NameTBLStrings[x], Id = x });
                }
            }

            return TblSections;
        }

        public static void SaveNameTBL(List<TblSection> tblSections, string outPath)
        {
            using (BinaryObjectWriter NAMETBLFile = new BinaryObjectWriter(outPath, Endianness.Big, AtlusEncoding.Persona5RoyalEFIGS))
            {
                for (int i = 0; i < tblSections.Count(); i++)
                {
                    List<long> StringPointers = new List<long>();

                    long fileSizePosition = NAMETBLFile.Position;
                    NAMETBLFile.WriteUInt32(0); // filesize

                    int numOfPointers = tblSections[i].TblEntries.Count;

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
                        NAMETBLFile.WriteString(StringBinaryFormat.NullTerminated, tblSections[i].TblEntries[j].ItemName);
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

        public static string pad_an_int(int N, int P)
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

        public static string GetTBLDirName(int tblNumber, int index)
        {
            if (tblNumber == 34)
            {
                return TblNames[index];
            }
            else return TblNamesR[index];
        }

        public static string[] TblNames = new string[] { "Arcanas", "Skills", "Enemies", "Personas", "Accessories", "Protectors", "Consumables",
                                               "Key Items", "Materials", "Melee Weapons", "Battle Actions", "Outfits", "Skill Cards",
                                               "Party FirstNames", "Party LastNames", "Confidant Names", "Ranged Weapons", "17",
                                               "18", "19", "20" };

        public static string[] TblNamesR = new string[] { "Arcanas", "Skills", "Skills Again", "Enemies", "Personas", "Traits", "Accessories",
                                                "Protectors", "Consumables", "Key Items", "Materials", "Melee Weapons", "Battle Actions",
                                                "Outfits", "Skill Cards", "Party FirstNames", "Party LastNames", "Confidant Names",
                                                "Ranged Weapons", "39", "39", "39", "39" };
    }

    public class TblSection
    {
        public string SectionName { get; set; } = "";
        public List<Entry> TblEntries { get; set; } = new List<Entry>();
    }

    public class Entry
    {
        public int Id { get; set; } = 0;
        public string ItemName { get; set; } = "";
        public string OldName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
