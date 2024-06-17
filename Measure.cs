using System.Collections.Generic;
using StereoKit;

public class Measure
{
    private Vec3 _measureStartPt;

    private Handed _measureHand = Handed.Max;

    private List<MeasureLine> _lines = new List<MeasureLine>();

    Pose menuPose = new Pose(0.5f, 0, -0.5f, Quat.LookDir(-1, 0, 1));

    public class MeasureLine
    {
        public Vec3 StartPoint;
        public Vec3 EndPoint;

        public void Draw()
        {
            Draw(new Color(0f, 0f, 0.1f));
        }

        public void Draw(Color clr)
        {
            Vec3 lineMdl = StartPoint + ((EndPoint - StartPoint) * .5f);
            Lines.Add(StartPoint, EndPoint, clr, 0.01f);
            Text.Add($"{Vec3.Distance(StartPoint, EndPoint) * 100f:F2}cm", Matrix.TR(lineMdl, Quat.LookAt(lineMdl, Input.Head.position)));
        }
    }
    public void Step()
    {
        if (_measureHand == Handed.Max)
        {
            for (int i = 0; i < (int)Handed.Max; i++)
            {
                var hand = Input.Hand((Handed)i);
                if (hand.IsJustPinched)
                {
                    if (UI.IsInteracting((Handed)i))break;
                    _measureHand = (Handed)i;
                    _measureStartPt = hand.pinchPt;
                }
            }
        }
        else
        {
            Lines.Add(_measureStartPt, Input.Hand(_measureHand).pinchPt, new Color32(0, 255, 0, 120), 0.01f);
            Vec3 lineMdl = _measureStartPt + ((Input.Hand(_measureHand).pinchPt - _measureStartPt) * .5f);
            Text.Add($"{Vec3.Distance(_measureStartPt, Input.Hand(_measureHand).pinchPt) * 100f:F2}cm", Matrix.TR(lineMdl, Quat.LookAt(lineMdl, Input.Head.position)));
            if (Input.Hand(_measureHand).IsJustPinched)
            {
                _lines.Add(new MeasureLine { StartPoint = _measureStartPt, EndPoint = Input.Hand(_measureHand).pinchPt });
                _measureHand = Handed.Max;
            }
        }

        _lines.ForEach(line => line.Draw());
        
        UI.WindowBegin("Menu", ref menuPose);
        UI.Label($"Lines:{_lines.Count}");
        if (_lines.Count > 0 && UI.Button("⬅️"))
        {
            _lines.RemoveAt(_lines.Count - 1);
        }
        UI.WindowEnd();
    }
}