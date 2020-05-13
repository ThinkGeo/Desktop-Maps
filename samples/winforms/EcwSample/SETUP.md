# Setup Mono runtime and MonoDevelop IDE
  1. Prepare a clean Ubuntu environment and the 14.04 is recommand.

  1. Press _Ctrl_ + _Alt_ + _T_ to open an terminal window. 

  1. Add the Mono Project GPG signing key and the package repository to your system (if you don’t use sudo, be sure to switch to root). Please issue flowing command line by line:
      ```perl
      sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF

      echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list

      sudo apt-get update
       ```
  1. To Install mod_mono, we will need to add a second repository to OS, issue following command in terminal. 
        ```perl
        echo "deb http://download.mono-project.com/repo/debian wheezy-apache24-compat main" | sudo tee -a /etc/apt/sources.list.d/mono-xamarin.list
        ```
  1. Install the mono environment, please issue following commands line by line, this step will take us few minitues depend on the network.
        ```perl
        sudo apt-get install mono-devel
    
        sudo apt-get install mono-complete
    
        sudo apt-get install referenceassemblies-pcl
        ```
    ***Notice***: Please enter **Y** when asking if you want to continue. *
  1. Install the IDE, MonoDevelop is recommend to develop on Linux. Run the following command to install **Mono Develop** on your computer.
	    ```perl
        sudo apt-get install monodevelop
        ```
  1. `new!` Then, to install the specific version of MonoDevelop that gives you a working ASP.NET add-in:
        ```perl
        sudo apt-get install monodevelop=5.10.0.871-0xamarin2
        ```
  1. `new!` To host the debug sample, install **XSP4**.
        ```perl
        sudo apt-get install mono-xsp4
        ```
  1. Run `monodevelop` command in terminal to open MonoDevelop; Or we can click on left top corner of task bar to search **MonoDevelop** and run it. We can open Visual Studio solution file via **MonoDevelop**.
 
# Download Source code
1. We've hosted the source code on github at [https://github.com/howardchn/Sample-HeatMap-WLM.git](https://github.com/howardchn/Sample-HeatMap-WLM.git). clone the repository to local by:
	```perl
    git clone https://github.com/howardchn/Sample-HeatMap-WLM.git
    ```
    On the other hand, you can download it as zip via click the clone or download button shown in following screen shot.

1. Also you can download the **SmartGit** to download folder, then you need to issue following command in terminal to start **SmartGit**:
	```perl
    cd ~/Download
    
    ./smartgit/bin/smartgit.sh 
    ```

# Add Library
1. We will config linux's library path contains MrSID unmanaged wrapper. After install the nuget package, when build the sample, unmanaged assemblies will be copy to alone with output directory(known as bin\Debug).
   
1. Got the directory for unmanaged assemblies that match with your processer, append the location to linux search directories list and save the file. 
    ```perl
    sudo gedit /etc/ld.so.conf.d/libc.conf
    ```

1. Reload the directory
    ```perl
    sudo ldconfig
    ```

# Add Local Nuget Source In MonoDevelop
1. Download the new Nuget package (10.0.300.2) and unpack it to your disk. If you cannot find this version, try to change the package version in the package.config from 10.0.300.2 to a specific version you have.
2. Open MonoDevelop preference dialog and navigate to the NuGet source manager as following screenshot.
![Preference](https://github.com/howardchn/Sample-HeatMap-WLM/blob/master/HeatStyle_OpenLayers/Content/NuGet-source-dialog.png?raw=true)
3. Click the Add button and the add package source dialog shows. Enter the source name and the directory path that Nuget packages are exacted to.  
![Add Local Source](https://github.com/howardchn/Sample-HeatMap-WLM/blob/master/HeatStyle_OpenLayers/Content/NuGet-source-add.png?raw=true)
4. Right click on the Packages solution folder in MonoDevelop likes screenshot bellow, then click restore button to restore the packages back or click Add Packages button to install new packages.  
![Restore Packages](https://github.com/howardchn/Sample-HeatMap-WLM/blob/master/HeatStyle_OpenLayers/Content/NuGet-source-restore.png?raw=true)
5. Press "F5" to launch the sample.

