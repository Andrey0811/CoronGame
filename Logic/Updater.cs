using System;
using System.Linq;
using System.Windows;
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
            for (var j = 0; j < enemies.Count; j++)
            {
                if (enemies[j].IsFreeze && enemies[j].Time <= 0)
                    enemies[j].IsFreeze = false;
                var isHit = HitGameObject(player, enemies[j]);


                if (isHit)
                {
                    --player.Life;
                    --playerLife.CountLife;
                    player.IsAlive = false;
                }

                for (var i = 0; i < blinds.Count; i++)
                {
                    if (!HitGameObject(blinds[i], enemies[j]))
                        continue;
                    blinds.RemoveAt(i);
                    --i;
                    enemies[j].IsFreeze = true;
                    enemies[j].Time = enemies[j].FreezeTime;
                }

                var isDelete = false;
                for (var i = 0; i < bullets.Count; i++)
                {
                    if (!HitGameObject(bullets[i], enemies[j]))
                        continue;
                    bullets.RemoveAt(i);
                    --i;
                    --enemies[j].Life;
                    if (enemies[j].Life > 0) continue;
                    enemies.RemoveAt(j);
                    isDelete = true;
                    --j;
                }

                if (isDelete) continue;

                if (FindPlayer(enemies[j]) && !isHit)
                {
                    var walker = new Walker(enemies[j].MoveDirection);
                    var temp = enemies[j].Point;
                    if (map.CanMoveOfPoint(temp, enemies[j].MoveDirection) && enemies[j].TimeBullet == 0)
                    {
                        temp.Offset(walker.X, walker.Y);
                        bullets.Add(map.InitBullet(temp, enemies[j].MoveDirection));
                        enemies[j].MoveDirection = InvertMoveDirection(enemies[j].MoveDirection);
                        enemies[j].TimeBullet = enemies[j].BulletTime;
                    }
                    else
                        --enemies[j].TimeBullet;
                }

                var tempDist = Math.Sqrt(
                    (player.Point.X - enemies[j].Point.X) * (player.Point.X - enemies[j].Point.X)
                    + (player.Point.Y - enemies[j].Point.Y) * (player.Point.Y - enemies[j].Point.Y));
                enemies[j].MoveDirection = tempDist < minDist
                    ? GenerateMoveToPlayer(enemies[j], true)
                    : GenerateMoveToPlayer(enemies[j], false);


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

            for (var i = 0; i < blinds.Count; i++)
            {
                if (!HitGameObject(blinds[i], player)) continue;
                blinds.RemoveAt(i);
                --i;
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
                for (var j = 0; j < cells.Count; j++)
                {
                    if (cells[j].IsFreeze && cells[j].Time <= 0)
                        cells[j].IsFreeze = false;

                    for (var i = 0; i < blinds.Count; i++)
                    {
                        if (!HitGameObject(blinds[i], cells[j])) continue;
                        blinds.RemoveAt(i);
                        --i;
                        cells[j].IsFreeze = true;
                        cells[j].Time = cells[j].FreezeTime;
                    }

                    var isDelete = false;
                    if (HitGameObject(player, cells[j]))
                    {
                        score.Value += cells[j].Award.Value;
                        currentScoreAward = cells[j].Award;
                        currentScoreAward.Point = cells[j].Point;
                        cells.RemoveAt(j);
                        --j;
                        isDelete = true;
                    }

                    if (isDelete) continue;

                    cells[j].MoveDirection = GenerateMoveToPlayer(cells[j], false);

                    if (!cells[j].IsFreeze)
                        cells[j].MakeMove();
                    else
                        --cells[j].Time;
                }
        }

        private void UpdateBullets()
        {
            for (var j = 0; j < bullets.Count; j++)
            {
                if (HitGameObject(player, bullets[j]))
                {
                    --player.Life;
                    --playerLife.CountLife;
                    player.IsAlive = false;
                    bullets.RemoveAt(j);
                    --j;
                    continue;
                }

                var isDelete = false;
                for (var i = 0; i < blinds.Count; i++)
                {
                    if (!HitGameObject(blinds[i], bullets[j]))
                        continue;
                    isDelete = true;
                    bullets.Remove(bullets[j]);
                    blinds.RemoveAt(i);
                    --j;
                    --i;
                }

                if (isDelete) continue;

                if (map.CanMove(bullets[j], bullets[j].MoveDirection))
                    bullets[j].MakeMove();
                else
                {
                    bullets.RemoveAt(j);
                    --j;
                }
            }
        }

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