﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using SagaBNS.Common.Packets;

using SmartEngine.Core;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

namespace SagaBNS.Common
{
    public class BNSGameNetwork<T> : Network<T>
    {
        bool ready = false;
        static Dictionary<string, List<byte[][]>> ExchangeKeys =new Dictionary<string,List<byte[][]>>();
        /*{
            //Lobby Gateway Handshakes        
            {"404051F1B965BA3D4457A02E6B973956AD93E86818627DF0C6AE8D2192F02B3F90A05C5095CC9B9E760F242EFC15505FB5BD457F45F44D2D7A4C434371BD5541BED2D853FDDDA499BCFB7663BA3E8393A3E2C524B40B41627B37958927887ADC8B786CA0FF46559B0BBABB239E7A38F3BF234895F5E61845F546FCCB526E1369D54098E1915752581FCE784F4227EB4495DDECF585D68BA5760E9D77901EBDB8B18D835DAAE52D128192B04C9ACF82636EF307484F818C7A9F11939E1E60F7227A97A98B39AD8192984C84F5A195CE86F59B7CAC37B4DCD832959095D9D8CD24156E63DE4D6CA96BD716B0E5727338C59A3E4EF23079B014592302916FA0F00B22C6",
            new string[]{"1050DDCAF1B74B1318605813935997A048D565D41BA4D7DE8499E5A39DE20BF01A1BFFAF12FD7821689CC36C286FF27DF4AA4D09E93AC503DEBF6745D91E7AA82732","3861196BB9195A87AC80D99B333A5313"}},
            
            //Game Server Handshakes        
            {"4040B5FA880C74D13C5B57B72464B2814A91DB1D3262EE15224E828BD21A8C52916C2E28450E4AC4BC38DC29F7765F5D99473D099C09117837AA967FE3A6786A249506B3885F635AD3B8A2A1A45CEBB1B2D2816886CAEB766F0DED52A4A9902502362289DBBD9B311078A336B02EEFBD91071BD8FFB4F5D8AFB5E41FE1CB5FD6DEA9AD9A4DEFB99C80D5BEBFB092B91B6BF5C8ABC7B59F46F0F9209605113BF7BDA9416E31CACCF7158C88BF3F5CF6416153B51922D41F9F22A3B00C73498CE51120782DA3C398A46A6A0AB9E68E1CBF1124D2A6DABF4B7C177614FABA51030D37B48474CB569A454A302D2B314378E227A2427DF1928BFAFF1573E850E3F183B583",
            new string[]{"10504FC99A4F9ECF80E7522BA9685D908E9AAD2DC84838E1958E554A32C8B9B28F334836ADD1575CB5359AA24070EEE995C14CFEDA735B0A6B0FDD9415AFCE2F69C1","469CF340EC466C8B390738B340BD6CA1"}},
            {"40409CA217698B7FAB714C32ADDE5D77FD3382785617827A377A28F7CD721949F09D3C48D35167AC4B78BD4FCE9A7FB73B1B4C370B983E47A7CBB50CD48FB80EDB860953367A3554D4D8547E3DD78456E9849BBCE7FB8735C8F7FE0FF12CBE15D335D8CA1840952DA8B3C2E3CE6A8642A6E9FB9FA71B6A796AF0A6E774ED8E7D85666BF80AE77BBFDC5DD109A75B026E87C2F260473D126E2EFE0CF34684A1312646ABAD321BADFB28A1A2B41488DAC64D2B0E166ED343AF11D8CB9E74A0FC14D28DE2A21BF6239C61FD785B05C62380F398751B767252DA65BB39F84945C4005A28CD0960705BA0B4A4EF02181F8BA0C488998ED817BFEB30580F8899712E58644A",
            new string[]{"1050265CCFE5D0194ED4B60053BECE2A7F8BED8D280970520028A5244F562790C81B80A3D3CCA39CDD6D6D855E3D54462FBE251E4D61B53B03426C6496DB70D88C94","EA161B6A42BCAA250E57CF1DF4619234"}},
            {"4040ECBDB459D25B24A7C97A07B98D2A7EAD86EEBC015878589AB540D562DE5BED510FA4A581C427290C2B48B5923EB387B05F4B17994CD567DA663A8C3BA99C3F1E14F23016E3CEA5E499D2CD8755C439FE9A36A0E7809ECBEB6CE5D80C4CE0C85480220845E38B6B6BE988B33F835AA1FBFFF0A2602E2B2D8C77154822874EF665B91D0B76E9CA6AED6CFCD813B8C321DFDF532695970D9E9FAE6988B90542F5C49674ECE1FD15896BFFB40F4F338E229BAEBE531304E1A3460A457A6591D6E51B128E7E8D095C431F6E83404D5A5F72351420A1FD5C9235DACA60380AD07A4D18A993DB2CC1BD223E105B0B5632172E4F6B8A0D4B2D2FFA93BCE4C9DC33BE6D20",
            new string[]{"105033B363232D9540E25DF96E6B88BB30D07438BB18B90A69B054822AF3E67EFD60227702A929BC2A8C3F29D108F4D149DF6DE29E93D6BF1216A4EF9BC389DC36C3","91BB60A391E3530F085180FD6E00E654"}},
        
            //Auction Server Handshakes
            {"4040CE74D07A9EC884C2A9A5EBBBF483179A2B16794964163F9D50856F568BE2CBBD857CAFCD7F86190B4F7C19D63F43FDDA04288F319154E5D3D5E7019D6282793D6D0D9C3612B70FB67812F96B11A8772C6A7416A956774E8F38C2D6D4A8D0645CA7A3BC2308617794C022BAE7FC66CB4D50D20327931A5817DE676AFBB3AF5CCA89A507FE88CA1A7ED2B1773E8E6AF7A1056A57F963392BA69F1C3762788D028D0F9E43C6738120F75FFF08D7436F1FBB654F182BA44142F5EEE4B609960142887D5D0986E1304285191F183E04EEC7050EC5989C303A5DBC9C778B5146D4293DC4449617B8314CD0A7133EF18FEE8778A573A5E325520E8E92C448841BE6DC6F",
            new string[]{"10501424307E8BEC83B824DBD8B5740D02D75C0ECAEE5E1BD77F96B828EF30E5F963D646EE07895352A98E8C0FCA0D65617F670A03B3DE7477E8DD2D162DA925B75C","D15ED6C33AE2433CBFC3E375CE40F214"}},
            {"404081A28DC6A51BF9FB80A49ACE84B7CF125BCD1FB777F1ED9429FE1A73E741290B58C788AED89B9D97FA8FC44864F404A31E4AC8C26CE54C7A666601A7D6629FB6A963E992FF03E733DBFE65138BF912C8A4523AF130F6B123F0667F947C89DADCECE6378A24218AEF048B3D977F544600B3CCA9575C4774D1E7632D6191675C0E41863238E719DB54AB257B78B600302A3BCEB2CAF433616296197D899AAC848C69E882CE1593F41068B410E19CCA12C7F0551AC43B2B1ED9078464CA96751F3AFECAC5BD18E6E9A7EA312E2185F29C2DE0998801B170493C8506F33D967FC1CE2E2F5038266CB9C8C1060BB0A216E753B755656D50A881D046F3B99DE8158D54",
            new string[]{"1050E4D8B93B29E2BA59859E1998A86AE1743458B1A2E09C2F4E9702241440EBC072FBA0DBB71B94CC2A3DA966C25BE93B07C949FEAB6B6299DE51E67A3478990692","7C2EA987CFE011F64559FEAACCB4359E"}},

/*
            Start of valid Game Server handshake though missing the auction server handshakes that go inbetween

            {"4040AC1D34B8CC25CA3411300A81CE7FCCA31EBFD6F61E19F3E42ABA8EE84C1D8F66E3F66EC6F3A663F080BF7D0B8448E943A4657FAEC253CE5EE753E5A020BDABA529EA755C820B4593BCF68C460ADCF2CE532E104463D6D0E3B19E0C3782ACF87889F1FDFB8E6DE416C12D9DD392F5AFA8DDCB3E923AFCDD1C248761BEAD4CA8B195DA6861CF44FBB074A2C7B6881FD401238A52076B3217CD34313F9E60C2856882A68817E3105ACFF33AFD72C2F121F4A402DE2289AF09574385960FC02E53FDF144643ECB6D8BC705D281DB16EBD2A25905A55B58AB82390A3F7E6F8115C61F22D3A5D202A73DAE5C7847E99BCD0942557CC3E990859C0D1BF8F0AD4E7C8D02",
            new string[]{"105054427F818930F00107379401A730882CF9AF2E7265FCDD4F63CEF0B4E1BFC11634377C1EEB0A3A3FBFDA42D68056889FA363F84BFB44EEA936A581DF98CCD2BA","00000000000000000000000000000000"}},
            {"4040CC4D716357F08F8EEA941515164371440F9AFAB0614AEBD06A0EF74C88BAD5BB1E8D4E7E0FC6B283128BDFAFEBE016A599E9F8128CC182610A5C7F277D1E61D491574854D603DE0804389D87D14769F3B35D36D30DB76F08B859ACA73A5E35F8439D634F541443CD0FF8FC90CD195900B88B2A299B36A3666240343D8E48897CEBAA259CA2D2E62A83E717B8E6D1A7F40344DD69FFD4011DB82A48C307D54EF3BEA885069BD970E03F2BE2F976FB344CCCDFFBB6D9EB624B081521101FA289D674A7C7D1589731B6F73E20BA53B59E81A69FB7D252756EB4357E43653B71E4DB8F5FCDA730219D4945728941FE268094BA2E074B3EEF534FA2AE427B64AFD3AC",
            new string[]{"10504C5D1EFDC39892E4BF06AC0994B31CBB6907EAA33058A02620AD30532F171BCE0BE2B6215498C53188A123397C78D3E8067AEDFE24F4573C47E1419B6451FC4A","00000000000000000000000000000000"}},
       
        };*/

        public static void AddKeyPair(string handshake, byte[][] keypair)
        {
            if (!ExchangeKeys.ContainsKey(handshake))
                ExchangeKeys.Add(handshake, new List<byte[][]>());
            ExchangeKeys[handshake].Add(keypair);
        }

        public byte[] ExchangeKey;

        public override Network<T> CreateNewInstance(System.Net.Sockets.Socket sock, Dictionary<T, Packet<T>> commandTable, Session<T> client)
        {
            BNSGameNetwork<T> instance = new BNSGameNetwork<T>();
            CreateNewInstance(instance, sock, commandTable, client);
            return instance;
        }

        protected override void OnReceivePacket(byte[] buf)
        {
            if (ready)
            {
                int totalLen = BitConverter.ToInt16(buf, 0);
                totalLen = (totalLen & 0xfff) * 4;
                if (buf.Length - 2 >= totalLen)
                {
                    try
                    {
                        byte[] buf2 = new byte[totalLen];
                        Array.Copy(buf, 2, buf2, 0, totalLen);
                        Crypt.Decrypt(buf2, 0, totalLen);
                        int len = BitConverter.ToInt16(buf2, 0) - 2;
                        byte[] tmp = new byte[len];
                        Array.Copy(buf2, 2, tmp, 0, len);
                        Packet<T> p = new Packet<T>();
                        p.PutBytes(tmp, 0);
                        ProcessPacket(p);
                        if (totalLen < buf.Length - 2)
                        {
                            int rest = buf.Length - 2 - totalLen;
                            buf2 = new byte[rest];
                            Array.Copy(buf, totalLen + 2, buf2, 0, rest);
                            OnReceivePacket(buf2);
                        }
                        else
                            lastContent = null;
                    }
                    catch (Exception)
                    {
                    }

                }
                else
                    lastContent = buf;
            }
            else
            {
                string handshake = Conversions.bytes2HexString(buf);
                List<byte[][]> list;
                if (ExchangeKeys.TryGetValue(handshake.Substring(260), out list))
                {
                    byte[][] keypair = list[Global.Random.Next(0, list.Count - 1)];
                    ExchangeKey = keypair[0];
                    ((Common.Encryption.BNSAESEncryption)Crypt).Key = keypair[1];
                    SendExchangePacket();
                }
                else
                {
                    Logger.ShowWarning(string.Format("Cannot find any keypair for handshake:{0}", handshake));
                    Disconnect();
                }
            }
        }


        public void SendExchangePacket()
        {
            SendPacketRaw(ExchangeKey, 0, ExchangeKey.Length);
            ready = true;
        }

        public override void SendPacket(Packet<T> p, bool noWarper)
        {
            throw new NotImplementedException();
        }

        public unsafe override void SendPacket(Packet<T> p)
        {
            if (Disconnected)
                return;
            int rest = 16 - ((int)(p.Length + 2) % 16);
            int oldSize = (int)p.Length;
            if (rest == 16)
                rest = 0;
            byte[] buf = new byte[p.Length + rest + 4];
            p.ToArray().CopyTo(buf, 4);
            fixed (byte* ptr = &buf[2])
            {
                *((ushort*)ptr) = (ushort)(oldSize + 2);
            }
            if (ready)
                Crypt.Encrypt(buf, 2, buf.Length - 2);
            fixed (byte* ptr = buf)
            {
                *((ushort*)ptr) = (ushort)(((buf.Length - 2) / 4) & 0xfff ^ 0x8000);
            }
            SendPacketRaw(buf, 0, buf.Length);
        }
    }
}