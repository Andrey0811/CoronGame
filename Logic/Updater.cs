using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CoronGame.Common;
using CoronGame.Models.Common;

namespace CoronGame.Logic
{
    public partial class Engine
    {
        private void UpdatePositions()
        {
            if (player.IsAlive)
            {
                UpdatePlayer();
                UpdateEnemeis();
                UpdateCells();
                UpdateBullets();
                UpdateBlinds();
            }
            else if (nextRoundTimeInterval <= 0)
            {
                nextRoundTimeInterval = 50;
                isGameStarted = false;
                if (player.Life <= 0)
                    InitGame();
                else
                    InitPlayers();
            }
            else
                nextRoundTimeInterval--;
        }

        private void UpdateEnemeis()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsFreeze && enemy.Time <= 0) 
                    enemy.IsFreeze = false;
                
                if (enemy.CanKill) { }
                else
                {
                    if (HitGameObject(player, enemy))
                    {
                        --player.Life;
                        player.IsAlive = false;
                    }

                    foreach (var blind in blinds.Where(blind => HitGameObject(blind, enemy)))
                    {
                        blinds.Remove(blind);
                        enemy.IsFreeze = true;
                        enemy.Time = enemy.FreezeTime;
                    }

                    /*foreach (var bullet in bullets.Where(bullet => HitGameObject(bullet, enemy)))
                    {
                        bullets.Remove(bullet);
                        --enemy.Life;
                        if (enemy.Life <= 0)
                            enemies.Remove(enemy);
                    }*/
                    var temp = Math.Sqrt(
                        (player.Point.X - enemy.Point.X) * (player.Point.X - enemy.Point.X)
                        + (player.Point.Y - enemy.Point.Y) * (player.Point.Y - enemy.Point.Y));
                    enemy.MoveDirection = temp < minDist
                        ? GenerateMoveToPlayer(enemy, true)
                        : GenerateMoveToPlayer(enemy, false);
                }

                if (FindPlayer(enemy))
                {
                    var walker = new Walker(enemy.MoveDirection);
                    var temp = enemy.Point;
                    temp.Offset(walker.X, walker.Y);
                    bullets.Add(map.InitBullet(temp, enemy.MoveDirection));
                }
                
                
                if (!enemy.IsFreeze) 
                    enemy.MakeMove();
                else
                    --enemy.Time;
            }
        }

        private void UpdatePlayer()
        {
            if (player.IsFreeze && player.Time <= 0) 
                player.IsFreeze = false;
            
            foreach (var blind in blinds.Where(blind => HitGameObject(blind, player)))
            {
                blinds.Remove(blind);
                player.IsFreeze = true;
                player.Time = player.FreezeTime;
            }

            if (!player.IsFreeze)
            {
                if (map.CanMove(player, playerNextMove))
                    player.MoveDirection = playerNextMove;

                if (map.CanMove(player, player.MoveDirection))
                    player.MakeMove();
            }
            else
                player.Time--;
        }

        private void UpdateCells()
        {
            if (cells.Count == 0)
            {
                if (rnd.Next(10000) < ChanceToSpawnCell)
                    cells.Add(map.InitCell());
            }
            else
                foreach (var cell in cells)
                {
                    if (cell.IsFreeze && cell.Time <= 0) 
                        cell.IsFreeze = false;
                    
                    foreach (var blind in blinds.Where(blind => HitGameObject(blind, cell)))
                    {
                        blinds.Remove(blind);
                        cell.IsFreeze = true;
                        cell.Time = cell.FreezeTime;
                    }
                    
                    if (HitGameObject(player, cell))
                    {
                        score.Value += cell.Award;
                        cells.Remove(cell);
                        return;
                    }

                    cell.MoveDirection = GenerateMoveToPlayer(cell, false);
                    
                    if (!cell.IsFreeze)
                        cell.MakeMove();
                    else
                        --cell.Time;
                }
        }

        private void UpdateBullets()
        {
            foreach (var bullet in bullets)
            {
                if (HitGameObject(player, bullet))
                {
                    --player.Life;
                    player.IsAlive = false;
                    bullets.Remove(bullet);
                    return;
                }

                foreach (var blind in blinds.Where(blind => HitGameObject(blind, bullet)))
                {
                    bullets.Remove(bullet);
                    blinds.Remove(blind);
                }
                
                if (map.CanMove(bullet, bullet.MoveDirection))
                    bullet.MakeMove();
                else
                    bullets.Remove(bullet);
            }
        }

        private void UpdateBlinds()
        {
            foreach (var blind in blinds)
            {
                if (HitGameObject(player, blind))
                {
                    player.IsFreeze = true;
                    blinds.Remove(blind);
                }
                
                if (map.CanMove(blind, blind.MoveDirection))
                    blind.MakeMove();
                else
                    blinds.Remove(blind);
            }
        }
    }
}