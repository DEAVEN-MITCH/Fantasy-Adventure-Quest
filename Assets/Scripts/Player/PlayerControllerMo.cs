pc.Rebounce+=Rebounce
Rebounce(){
    if(!onAction){
    onAction=true;
    isRebounce=true;
    }
}
Animator:{
    Update(){
        anim.SetBool("rebounce",isRebounce);
    }
}
Animation:{
    OnExit(){
        isRebounce=false;
    }
    OnEnter(){
        isRebounce=true;
    }
}
pc:find Rebounce localScale=sr.flipX?……
// Animator +一个新的空状态用于受伤退出将OnAction置False.正常仅Rebounce3退出将OnAction置False
//Animation 关键帧调用Rebounce.Activate()和Rebounce.DeActivate()来激活和关闭碰撞体