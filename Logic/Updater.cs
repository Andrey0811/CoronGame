using System;
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
                //UpdateBullets();
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
            for (var j = 0; j < enemies.Count; j++)
            {
                if (enemies[j].IsFreeze && enemies[j].Time <= 0)
                    enemies[j].IsFreeze = false;
                var isHit = HitGameObject(player, enemies[j]);

                if (enemies[j].CanKill)
                {
                }
                else
                {
                    if (isHit)
                    {
                        --player.Life;
                        player.IsAlive = false;
                    }

                    for (var i = 0; i < blinds.Count; i++)
                    {
                        if (!HitGameObject(blinds[i], enemies[j]))
                            continue;
                        blinds.RemoveAt(i);
                        enemies[j].IsFreeze = true;
                        enemies[j].Time = enemies[j].FreezeTime;
                    }

                    for (var i = 0; i < bullets.Count; i++)
                    {
                        if (!HitGameObject(bullets[i], enemies[j]))
                            continue;
                        bullets.RemoveAt(i);
                        --enemies[j].Life;
                        if (enemies[j].Life <= 0) 
                            enemies.RemoveAt(j);
                    }

                    var temp = Math.Sqrt(
                        (player.Point.X - enemies[j].Point.X) * (player.Point.X - enemies[j].Point.X)
                        + (player.Point.Y - enemies[j].Point.Y) * (player.Point.Y - enemies[j].Point.Y));
                    enemies[j].MoveDirection = temp < minDist
                        ? GenerateMoveToPlayer(enemies[j], true)
                        : GenerateMoveToPlayer(enemies[j], false);
                }

                if (FindPlayer(enemies[j]) && !isHit)
                {
                    var walker = new Walker(enemies[j].MoveDirection);
                    var temp = enemies[j].Point;
                    temp.Offset(walker.X, walker.Y);
                    bullets.Add(map.InitBullet(temp, enemies[j].MoveDirection));
                }


                if (!enemies[j].IsFreeze)
                    enemies[j].MakeMove();
                else
                    --enemies[j].Time;
            }
        }

        private void UpdatePlayer()
        {
            if (player.IsFreeze && player.Time <= 0) 
                player.IsFreeze = false;
            
            /*foreach (var blind in blinds.Where(blind => HitGameObject(blind, player)))
            {
                blinds.Remove(blind);
                player.IsFreeze = true;
                player.Time = player.FreezeTime;
            }*/

            if (!player.IsFreeze)
            {
                if (map.CanMove(player, playerNextMove))
                    player.MoveDirection = playerNextMove;

                if (map.CanMove(player, player.MoveDirection))
                    player.MakeMove();
            }
            else
                --player.Time;
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

                    for (var i = 0; i < blinds.Count; i++)
                    {
                        if (!HitGameObject(blinds[i], cell)) continue;
                        blinds.RemoveAt(i);
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

        /*private void UpdateBullets()
        {
            for (var j = 0; j < bullets.Count; j++)
            {
                if (HitGameObject(player, bullets[j]))
                {
                    --player.Life;
                    player.IsAlive = false;
                    bullets.RemoveAt(j);
                    continue;
                }

                var isLoop = false;
                for (var i = 0; i < blinds.Count; i++)
                {
                    if (!HitGameObject(blinds[i], bullets[j]))
                        continue;
                    isLoop = true;
                    bullets.Remove(bullets[j]);
                    blinds.RemoveAt(i);
                }
                
                if (isLoop) continue;

                if (map.CanMove(bullets[j], bullets[j].MoveDirection))
                    bullets[j].MakeMove();
                else
                    bullets.RemoveAt(j);
            }
        }*/

        private void UpdateBlinds()
        {
            for (var i = 0; i < blinds.Count; i++)
            {
                if (HitGameObject(player, blinds[i]))
                {
                    player.IsFreeze = true;
                    blinds.RemoveAt(i);
                    continue;
                }

                if (map.CanMove(blinds[i], blinds[i].MoveDirection))
                    blinds[i].MakeMove();
                else
                    blinds.Remove(blinds[i]);
            }
        }
    }
}