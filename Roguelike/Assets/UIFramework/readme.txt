一些更该：
Prefab/UI/Dark Screen  -> UIFramework/Resourrces/Prefab/Effect
Assets/Script/UI/UI_Fade_Screen -> Assets/UIFramework/Scripts/Effect/UI_Fade_Screen.cs
删除 Assets/Script/UI/UI_ChoosePart.cs

需要在Assets下创建Resource/Panel专门放预制体Panel

UIFramework中Core为核心部分，一般不用动
Core整体分为两个部分，
UIFrame负责管理UI和面板
SceneFrame负责场景相关

在这个框架中UI的基本单位是一个面板（也就是一个Canvas预制体），最终的效果可以实现在场景中做Panel，设置成预制体，然后稍微添加代码即可加入整体UI的可拓展性框架

整体思路是模拟栈的方式，将Panel作为基本元素先进先出的形式写成

以下是大概说明各类，代码中也有一些对应注释

UIType：存储UI元素名字和路径信息，所以预制体Panel的路径一般不要随便改动，要放在Assets/Resources/Panel文件夹下
BasePanel：Panle父类，有启动，继续（暂停后继续该面板），禁用（暂停时禁用），摧毁四个状态
UIManager：UI管理器，模拟栈的核心部分，面板关于栈的操作一般也是调用该类，基本是外部最常调用的类
UIMethods：基本不用管，用来放一些UICore中常用的方法

SceneBase：场景基类，存储场景名字以及场景进入，退出方法
SceneControl：场景控制器，主要是加载新场景时不用SceneManager.LoadScene方法，而是用SceneControl.GetInstance().LoadScene()，自己写的修饰后的加载场景方法，

Core中的类都是单例模式，用类名.GetInstance()就可以调用其中的方法

GameRoot所在的场景进入开始模式后会自动触发进入该场景的方法

后续要加入UI框架的每一个场景都要有对应同名的场景类放在SceneFrame中，每一个Panel都要有对应同名的Panel代码放在UIFrame中，Panel预制体在Resources/Prefab/Panel 中

