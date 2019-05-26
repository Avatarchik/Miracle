
错误情况：从远程仓库克隆下来的Pro2CS.dll和Pro2CS.pdb文件到mac后发生改变，导致不能生成代码。  
造成原因：生成代码时会报win32异常。  
个人分析：需要源代码在mac上生成Pro2CS.dll和Pro2CS.pdb文件才能在mac上使用。  
临时解决办法：故压缩了一个用于在mac上生成协议的压缩包，在mac上解压后用MacOS_Start中的Proto2CS.command生成最新协议代码即可。  

在mac上开发时保持ProtoforMac中的协议是最新的，在win上开发时保持Proto中的协议是最新的即可。