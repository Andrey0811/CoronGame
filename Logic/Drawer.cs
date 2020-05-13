using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CoronGame.Models.Interfaces;

namespace CoronGame.Logic
{
    public partial class Engine 
    {
        public void DrawObj()
        {
            render.Clear();
            render.Draw(score.Format(), score.Point, score.Size);
            Render(map);
            render.Draw(field.Figure, field.Point, field.Size);
            RenderCollection(enemies);
            RenderCollection(cells);
            RenderCollection(blinds);
            RenderCollection(bullets);
            //RenderCollection(lifes);
            Render(player);
            
            if (currentScoreAward == null) 
                return;
            if (currentScoreAward.IsValid)
                render.Draw(currentScoreAward.Format(), currentScoreAward.Point, currentScoreAward.Size);
            else
                currentScoreAward = null;

            /*for (var i = 0; i < playerLife.CountLife; i++)
            {
                playerLife.Point = new Point(map.DistanceBetweenObj + i * 30, playerLife.Point.Y);
                render.Draw(playerLife.Image, playerLife.Point, playerLife.Size);
            }*/
        }

        private void Render<T>(T obj) where T : IImagable, IGameObject, IJump
        {
            if (obj == null) return;
            if (obj.IsJumped)
            {
                obj.Point = new Point(obj.Point.X, obj.Point.Y + obj.JumpHeight);
                render.Draw(obj.GetImage(), obj.Point, obj.Size);
                obj.Point = new Point(obj.Point.X, obj.Point.Y - obj.JumpHeight);
                return;
            }
            render.Draw(obj.GetImage(), obj.Point, obj.Size);
        }

        private void RenderCollection<T>(IEnumerable<T> objs) where T : IImagable, IGameObject, IJump
        {
            foreach (var obj in objs) Render(obj);
        }
    }
}