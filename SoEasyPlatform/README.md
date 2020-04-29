### SoEasyPlatform 代码生成器

## 介绍
一款轻量级开源的代码生成器，相对较动软代码生成器而言要轻量的多，支持多种数据库，所用到dll组件也都在github有源码，代码非常的简单有点基础的看源码可以把生成的项目改成自已的风格。

 
## 特色
该代码生成器最大的特点就三个简单 ，无需安装，生成的代码 简单并且有教学用例，还有就是调试和修改模版简单。

 
## 使用步骤
1.从上面的地址下载 SoEasyPlatform到本地  


2.解压项目

点击SoEasyPlatform.sln打开项目，重新生成项目会自动下载NUGET 文件 


3.配置三个参数

const SqlSugar.DbType dbType = DbType.SqlServer;//数据库类型

const string connectionString = "server=.;uid=sa;pwd=@jhl85661501;database=SqlSugar4XTest";//连接字符串

const string SolutionName = "SoEasyPlatform";//解决方案名称
　　

4.F5运行


5.完成

我们发现两个类库已经添加到解决方案下面了,并且相关的dll的类库引用也帮我们做好了，非常方便，数据库有改动后F5刷新就好了。

执行完成没发现有类库加进来，关掉解决方案重新打开便可以了


## 如何使用生成的代码开发项目

 1.新建一个项目

  Web项目或者控制台都可以


 2.引用生成的类库


 3.代码如下

 StudentManager m = new StudentManager();

