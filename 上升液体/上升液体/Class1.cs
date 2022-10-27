
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using Terraria.UI;
using System.Linq.Expressions;
using Terraria.ID;
using System.Threading.Tasks;
using System.Security.Policy;

namespace 上升液体
{
    [ApiVersion(2, 1)]//api版本
    public class 上升液体 : TerrariaPlugin
    {
        /// 插件作者
        public override string Author => "奇威复反";
        /// 插件说明
        public override string Description => "液体移动";
        /// 插件名字
        public override string Name => "使液体移动";
        /// 插件版本
        public override Version Version => new Version(1, 0, 0, 0);
        /// 插件处理
        public 上升液体(Main game) : base(game)
        {
        }
        /// 插件启动时，用于初始化各种狗子
        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);//钩住游戏初始化时
        }
        /// 插件关闭时
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Deregister hooks here
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);//销毁游戏初始化狗子

            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)//游戏初始化的狗子
        {
            //第一个是权限，第二个是子程序，第三个是名字

            Commands.ChatCommands.Add(
                new Command("液体", _替, "液体")
                {
                });
        }
        private async void _替(CommandArgs args)
        {
            try
            {
                int x = 0;
                byte yt = (byte)Convert.ToSByte(args.Parameters[1]);
                int y = Convert.ToInt16(args.Parameters[2]);
                int yz = Convert.ToInt16(args.Parameters[3]);
                int t = (int)(float.Parse(args.Parameters[4]) * 1000);
                TSPlayer.All.SendErrorMessage($"液体移动开始了.");
                switch (args.Parameters[0])
                {

                    case "上":
                        while (y >= yz)
                        {
                            if (x == WorldGen.WorldSizeLargeX)
                            {
                                x = 0;
                                y--;
                                WorldGen.PlaceLiquid(x, y, yt, 255);
                                //刷新物块();
                                await Task.Delay(t);
                            }
                            else
                                WorldGen.PlaceLiquid(x, y, yt, 255);

                            x++;
                        }
                        TSPlayer.All.SendErrorMessage($"液体上升结束了...");
                        break;
                    case "下":
                        while (y <= yz)
                        {
                            if (x == WorldGen.WorldSizeLargeX)
                            {
                                x = 0;
                                y++;
                                WorldGen.PlaceLiquid(x, y, yt, 255);
                                //刷新物块();
                                await Task.Delay(t);
                            }
                            else
                                WorldGen.PlaceLiquid(x, y, yt, 255);

                            x++;
                        }
                        TSPlayer.All.SendErrorMessage($"液体下降结束了...");
                        break;
                    case "右":
                        while (y <= yz)
                        {
                            if (x == WorldGen.WorldSizeLargeY)
                            {
                                x = 0;
                                y++;
                                WorldGen.PlaceLiquid(y, x, yt, 255);
                                //刷新物块();
                                await Task.Delay(t);
                            }
                            else
                                WorldGen.PlaceLiquid(y, x, yt, 255);

                            x++;
                        }
                        TSPlayer.All.SendErrorMessage($"液体右移结束了...");
                        break;
                    case "左":
                        while (y >= yz)
                        {
                            if (x == WorldGen.WorldSizeLargeY)
                            {
                                x = 0;
                                y--;
                                WorldGen.PlaceLiquid(y, x, yt, 255);
                                //刷新物块();
                                await Task.Delay(t);
                            }
                            else
                                WorldGen.PlaceLiquid(y, x, yt, 255);

                            x++;
                        }
                        TSPlayer.All.SendErrorMessage($"液体左移结束了...");
                        break;
              /*      case "停":
                    case "止":
                    case "stop":
                        y = yz;
                        TSPlayer.All.SendSuccessMessage($"以强制所有停止液体移动");
                        break;
              */
                }
            }
            catch
            {
                try
                {
                    switch (args.Parameters[0])
                    {
                        case "方向":
                            args.Player.SendSuccessMessage($"方向：上 下 左 右");
                            break;
                        case "帮助":
                        case "help":
                            args.Player.SendSuccessMessage($"正确指令：/液体 上 液体种类ID 上升启始高度 终止高度 上升间隔(秒) ");
                            args.Player.SendSuccessMessage($"正确指令：/液体 下 液体种类ID 下降启始高度 终止高度 上升间隔(秒) ");
                            args.Player.SendSuccessMessage($"正确指令：/液体 右 液体种类ID 右移启始位置 终止位置 移动间隔(秒) ");
                            args.Player.SendSuccessMessage($"正确指令：/液体 左 液体种类ID 左移启始位置 终止位置 移动间隔(秒) ");
                            break;
                        default:
                            args.Player.SendSuccessMessage($"指令格式：/液体 方向 液体种类ID 启始位置 终止位置 移动间隔(秒) ");
                            args.Player.SendSuccessMessage($"液体种类：水=0 岩浆=1 蜂蜜=2 微光=3 ");
                            args.Player.SendSuccessMessage($"输入/pos 查看当前位置的高度");
                            break;
                    }
                }
                catch
                {
                    args.Player.SendSuccessMessage($"指令格式：/液体 方向 液体种类ID 启始位置 终止位置 移动间隔(秒) ");
                    args.Player.SendSuccessMessage($"液体种类：水=0 岩浆=1 蜂蜜=2 微光=3 ");
                    args.Player.SendSuccessMessage($"输入/pos 查看当前位置的高度");
                }

            }

        }
        /* 
         * public static void 刷新物块()
         {
             foreach (TSPlayer person in TShock.Players)
             {
                 if ((person != null) && (person.Active))
                 {
                     for (int i = 0; i < 255; i++)
                     {
                         for (int j = 0; j < Main.maxSectionsX; j++)
                         {
                             for (int k = 0; k < Main.maxSectionsY; k++)
                             {
                                 Netplay.Clients[i].TileSections[j, k] = false;
                             }
                         }
                     }
                 }
             }
         }*/

    }
}