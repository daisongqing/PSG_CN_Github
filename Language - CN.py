#将中文的设计文件批量换成默认

import os
import shutil

# 查找该层文件夹下指定文件
def search_file(dirPath, fileName):
    dirs = os.listdir(dirPath) # 查找该层文件夹下所有的文件及文件夹，返回列表
    for currentFile in dirs: # 遍历列表
        absPath = dirPath + '/' + currentFile # 文件的绝对路径
        #if currentFile == fileName:
        if currentFile.endswith('.cs') and fileName in currentFile:
             print('--> '+absPath)
             print('--+ '+absPath[:-8]+'.cs')
             shutil.copy2(absPath, absPath[:-8]+'.cs')
           
if __name__ == "__main__":
    fileName = '- CN'
    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.UI.Standalone/Block', fileName)
    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.UI.Standalone/FunctionControls', fileName)
    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.UI.Standalone/FunctionControls/tools', fileName)
    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.UI.Standalone/EnumModel', fileName)
    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.UI.Standalone', fileName)

    search_file('/Users/jason/Jason/PSG/PSG_CN/AwareTec.Polysmith.pChart', fileName)