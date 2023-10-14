﻿using OpenTK.Mathematics;

namespace _2lab.Objects;

public interface IObject
{
    public void Render(Camera camera, Vector3 lightPos, Vector3 position, float angle)
    {
        
    }
    
    public void TurnOnFlashlight()
    {
        
    }
    
    public void TurnOffFlashlight()
    {
        
    }
    
    public void TurnOnPointlight()
    {
        
    }
    
    public void TurnOffPointlight()
    {
        
    }
    
    public void TurnOnDirlight()
    {
        
    }
    
    public void TurnOffDirlight()
    {
        
    }
}