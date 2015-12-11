﻿namespace ComputationalGeometry.PointLocation
{
    public interface IFace
    {
        ISegment TopSegment
        {
            get;
        }

        ISegment BottomSegment
        {
            get;
        }
        
        IVerticalEdge LeftEdge
        {
            get;
        }

        IVerticalEdge RightEdge
        {
            get;
        }
    }
}
