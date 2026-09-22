# ==================== 配置区域 ====================
$NETWORK = "my-network"
$COMMON_DB_PASSWORD = "sasa"

# PostgreSQL
$PG_CONTAINER = "Postgres"
$PG_IMAGE = "postgres:16"
$PG_USER = "postgres"
$PG_PASSWORD = $COMMON_DB_PASSWORD
$PG_VOLUME_HOST = "D:\VolumesDocker\postgres_data"
$PG_VOLUME_CONTAINER = "/var/lib/postgresql/data"
$PG_PORT_HOST = "5432"
$PG_PORT_CONTAINER = "5432"
$PG_DB_NAME = "erp"

# MySQL/MariaDB
$MYSQL_CONTAINER = "Mariadb"
$MYSQL_IMAGE = "mariadb:lts-jammy"
$MYSQL_ROOT_PASSWORD = $COMMON_DB_PASSWORD
$MYSQL_VOLUME_HOST = "D:\VolumesDocker\mariadb"
$MYSQL_VOLUME_CONTAINER = "/var/lib/mysql"
$MYSQL_PORT_HOST = "3306"
$MYSQL_PORT_CONTAINER = "3306"
$MYSQL_DB_NAME = "erp"

# SQL Server
$MSSQL_CONTAINER = "SqlServer"
$MSSQL_IMAGE = "mcr.microsoft.com/mssql/server:2019-latest"
$MSSQL_SA_PASSWORD = "sasa@123456"
$MSSQL_VOLUME_HOST = "D:\VolumesDocker\sqlserver"
$MSSQL_VOLUME_CONTAINER = "/var/opt/mssql"
$MSSQL_PORT_HOST = "1433"
$MSSQL_PORT_CONTAINER = "1433"
$MSSQL_DB_NAME = "erp"

# phpMyAdmin (仅在 MySQL 分支可选)
$PMA_CONTAINER = "phpMyAdmin"
$PMA_IMAGE = "phpmyadmin:latest"
$PMA_PORT_HOST = "8081"
$PMA_PORT_CONTAINER = "80"

# WebAPI 镜像源
$API_CONTAINER = "Erp.WebAPI"
$API_IMAGE_OFFICIAL = "doipc/erpwebapi:latest"
$API_IMAGE_ALIYUN = "crpi-ul14z15hxdr420vg.cn-hangzhou.personal.cr.aliyuncs.com/doipc/erpwebapi:latest"
$API_IMAGE_TENCENT = "ccr.ccs.tencentyun.com/doipc/erpwebapi:latest"
$API_IMAGE_HUAWEI = "swr.cn-east-3.myhuaweicloud.com/doipc/erpwebapi:latest"

$UPLOAD_HOST_DIR = "D:\VolumesDocker\appupload"
$API_VOLUME = "${UPLOAD_HOST_DIR}:/userData"
$API_HOST_PORT = "80"
$API_CONTAINER_PORT = "8080"
# =================================================

# 颜色输出辅助函数
function Write-ColorOutput {
    param(
        [string]$ForegroundColor,
        [string]$Message
    )
    Write-Host $Message -ForegroundColor $ForegroundColor
}

# 带超时的输入函数（支持回车确认、退格、回显）
function Read-HostWithTimeout {
    param(
        [string]$Prompt,
        [int]$TimeoutSeconds,
        [string]$DefaultValue
    )
    Write-Host $Prompt -NoNewline
    $timer = [System.Diagnostics.Stopwatch]::StartNew()
    $inputString = [System.Text.StringBuilder]::new()
    while ($timer.Elapsed.TotalSeconds -lt $TimeoutSeconds) {
        if ([Console]::KeyAvailable) {
            $key = [Console]::ReadKey($false)  # 显示按键
            if ($key.Key -eq [ConsoleKey]::Enter) {
                $timer.Stop()
                Write-Host  # 换行
                if ($inputString.Length -eq 0) {
                    return $DefaultValue
                } else {
                    return $inputString.ToString()
                }
            } elseif ($key.Key -eq [ConsoleKey]::Backspace) {
                if ($inputString.Length -gt 0) {
                    $inputString.Length--
                    Write-Host "`b `b" -NoNewline
                }
            } else {
                $char = $key.KeyChar
                $inputString.Append($char) | Out-Null
            }
        }
        Start-Sleep -Milliseconds 50
    }
    $timer.Stop()
    Write-Host  # 换行
    if ($inputString.Length -eq 0) {
        return $DefaultValue
    } else {
        return $inputString.ToString()
    }
}

Write-Host "========================================="
Write-Host "  Erp 2.0 WebAPI 全栈依赖一键部署"
Write-Host "  支持: PostgreSQL / MySQL / SQL Server"
Write-Host "========================================="

# ---- 1. 创建网络 ----
Write-ColorOutput Yellow "[1/10] 创建 Docker 网络: $NETWORK ..."
$networkExists = docker network inspect $NETWORK 2>$null
if ($LASTEXITCODE -eq 0) {
    Write-ColorOutput Yellow "  ℹ️  网络 $NETWORK 已存在，跳过创建"
} else {
    docker network create $NETWORK
    Write-ColorOutput Green "  ✅ 网络 $NETWORK 创建成功"
}

# ---- 2. 选择数据库类型 ----
Write-ColorOutput Yellow "[2/10] 请选择要部署的数据库类型（30秒内无输入默认选择 PostgreSQL）"
Write-Host "  1) PostgreSQL (默认)"
Write-Host "  2) MySQL/MariaDB"
Write-Host "  3) SQL Server"
Write-Host "  [提示] 请输入数字（1/2/3）后按回车确认。"

$choice = Read-HostWithTimeout -Prompt "请输入选项 [1-3]: " -TimeoutSeconds 30 -DefaultValue "1"
Write-Host "您选择了: $choice"

if ($choice -eq $null -or $choice -eq "") {
    $choice = "1"
    Write-ColorOutput Yellow "  ⏰ 超时未输入，自动选择 PostgreSQL"
}

$DB_TYPE = ""
$DB_CONTAINER = ""
$DB_IMAGE = ""
$DB_PORT_HOST = ""
$DB_PORT_CONTAINER = ""
$DB_VOLUME_HOST = ""
$DB_VOLUME_CONTAINER = ""
$DB_PROVIDER = ""
$DB_CONNECTION_STRING = ""
$DB_ENV = @()

switch ($choice) {
    "1" {
        $DB_TYPE = "postgres"
        $DB_CONTAINER = $PG_CONTAINER
        $DB_IMAGE = $PG_IMAGE
        $DB_PORT_HOST = $PG_PORT_HOST
        $DB_PORT_CONTAINER = $PG_PORT_CONTAINER
        $DB_VOLUME_HOST = $PG_VOLUME_HOST
        $DB_VOLUME_CONTAINER = $PG_VOLUME_CONTAINER
        $DB_PROVIDER = "PostgreSql"
        $DB_CONNECTION_STRING = "Host=${DB_CONTAINER};Port=${DB_PORT_CONTAINER};Database=${PG_DB_NAME};Username=${PG_USER};Password=${PG_PASSWORD}"
        $DB_ENV = @(
            "-e", "POSTGRES_USER=$PG_USER",
            "-e", "POSTGRES_PASSWORD=$PG_PASSWORD",
            "-e", "POSTGRES_DB=$PG_DB_NAME"
        )
    }
    "2" {
        $DB_TYPE = "mysql"
        $DB_CONTAINER = $MYSQL_CONTAINER
        $DB_IMAGE = $MYSQL_IMAGE
        $DB_PORT_HOST = $MYSQL_PORT_HOST
        $DB_PORT_CONTAINER = $MYSQL_PORT_CONTAINER
        $DB_VOLUME_HOST = $MYSQL_VOLUME_HOST
        $DB_VOLUME_CONTAINER = $MYSQL_VOLUME_CONTAINER
        $DB_PROVIDER = "MySql"
        $DB_CONNECTION_STRING = "server=${DB_CONTAINER};port=${DB_PORT_CONTAINER};user=root;password=${MYSQL_ROOT_PASSWORD};database=${MYSQL_DB_NAME}"
        $DB_ENV = @(
            "-e", "MYSQL_ROOT_PASSWORD=$MYSQL_ROOT_PASSWORD",
            "-e", "MYSQL_DATABASE=$MYSQL_DB_NAME"
        )
    }
    "3" {
        $DB_TYPE = "sqlserver"
        $DB_CONTAINER = $MSSQL_CONTAINER
        $DB_IMAGE = $MSSQL_IMAGE
        $DB_PORT_HOST = $MSSQL_PORT_HOST
        $DB_PORT_CONTAINER = $MSSQL_PORT_CONTAINER
        $DB_VOLUME_HOST = $MSSQL_VOLUME_HOST
        $DB_VOLUME_CONTAINER = $MSSQL_VOLUME_CONTAINER
        $DB_PROVIDER = "SqlServer"
        $DB_CONNECTION_STRING = "Server=${DB_CONTAINER},${DB_PORT_CONTAINER};Database=${MSSQL_DB_NAME};User Id=sa;Password=${MSSQL_SA_PASSWORD};TrustServerCertificate=True;"
        $DB_ENV = @(
            "-e", "ACCEPT_EULA=Y",
            "-e", "SA_PASSWORD=$MSSQL_SA_PASSWORD",
            "-e", "MSSQL_PID=Express"
        )
    }
    default {
        Write-ColorOutput Red "❌ 无效选项，退出脚本"
        exit 1
    }
}

Write-ColorOutput Green "✅ 选择数据库: $DB_TYPE (容器: $DB_CONTAINER)"

# ---- 3. 询问是否删除原有数据 ----
Write-ColorOutput Yellow "[3/10] 是否删除原有数据库数据？"
Write-Host "  ⚠️  若选择删除，将清除数据卷目录 $DB_VOLUME_HOST 中的所有数据，且不可恢复。"
Write-Host "  默认保留（不删除），30秒内无输入自动保留。"
Write-Host "  [提示] 请输入 Y (删除) 或 N (保留) 后按回车确认。"

$deleteInput = Read-HostWithTimeout -Prompt "是否删除数据? [y/N]: " -TimeoutSeconds 30 -DefaultValue "N"
Write-Host "您选择了: $deleteInput"

$DELETE_DATA = $false
if ($deleteInput -match "^[yY]$") {
    $DELETE_DATA = $true
}

if ($DELETE_DATA) {
    Write-ColorOutput Red "  ⚠️  已选择删除数据，稍后将清除数据卷目录。"
} else {
    Write-ColorOutput Green "  ✅ 保留数据卷目录。"
}

# ---- 4. 清理旧数据库容器及数据卷 ----
Write-ColorOutput Yellow "[4/10] 清理旧的 $DB_CONTAINER 容器及数据卷..."
$oldDb = docker ps -a -q -f "name=^${DB_CONTAINER}$"
if ($oldDb) {
    Write-Host "  → 发现旧容器 (ID: $oldDb)，正在停止并删除..."
    docker stop $oldDb 2>$null
    docker rm $oldDb 2>$null
    Write-ColorOutput Green "  ✅ 旧 $DB_CONTAINER 容器已删除"
} else {
    Write-ColorOutput Yellow "  ℹ️  未找到名为 $DB_CONTAINER 的容器"
}

if ($DELETE_DATA) {
    if (Test-Path $DB_VOLUME_HOST) {
        Write-Host "  → 删除数据卷目录: $DB_VOLUME_HOST"
        Remove-Item -Recurse -Force $DB_VOLUME_HOST -ErrorAction SilentlyContinue
        if (Test-Path $DB_VOLUME_HOST) {
            Write-ColorOutput Red "  ❌ 删除失败，目录仍然存在。请手动处理。"
            exit 1
        } else {
            Write-ColorOutput Green "  ✅ 数据卷已删除"
        }
    } else {
        Write-ColorOutput Yellow "  ℹ️  数据卷目录不存在，无需删除"
    }
} else {
    Write-ColorOutput Green "  ✅ 保留数据卷（$DB_VOLUME_HOST）"
}

# ---- 5. 创建数据目录 ----
Write-ColorOutput Yellow "[5/10] 创建数据库数据目录..."
New-Item -ItemType Directory -Force -Path $DB_VOLUME_HOST | Out-Null
Write-ColorOutput Green "  ✅ 数据目录已准备: $DB_VOLUME_HOST"

# ---- 6. 启动数据库容器 ----
Write-ColorOutput Yellow "[6/10] 启动 $DB_CONTAINER 容器..."
$dockerVolumePath = $DB_VOLUME_HOST -replace '\\', '/'
$dockerArgs = @(
    "run", "--name", $DB_CONTAINER,
    "--network", $NETWORK
) + $DB_ENV + @(
    "-v", "${dockerVolumePath}:${DB_VOLUME_CONTAINER}",
    "-p", "${DB_PORT_HOST}:${DB_PORT_CONTAINER}",
    "-d", $DB_IMAGE
)
& docker $dockerArgs

Start-Sleep -Seconds 3
$running = (docker inspect -f '{{.State.Running}}' $DB_CONTAINER 2>$null).Trim()
if ($running -ne "true") {
    Write-ColorOutput Red "  ❌ 数据库容器未能启动，请查看日志：docker logs $DB_CONTAINER"
    exit 1
} else {
    Write-ColorOutput Green "  ✅ $DB_CONTAINER 已启动（端口: $DB_PORT_HOST）"
}

# ---- 7. 等待数据库就绪 ----
Write-Host "  ⏳ 等待数据库就绪..."
switch ($DB_TYPE) {
    "postgres" {
        $readyFlag = $false
        for ($i = 0; $i -lt 30; $i++) {
            $ready = docker exec $DB_CONTAINER pg_isready -U $PG_USER 2>&1
            if ($LASTEXITCODE -eq 0) {
                Write-ColorOutput Green "  ✅ PostgreSQL 已就绪"
                $readyFlag = $true
                break
            }
            if ($i -eq 0) { Write-Host "  ... 等待 PostgreSQL 启动，输出：$ready" }
            Start-Sleep -Seconds 2
        }
        if (-not $readyFlag) {
            Write-ColorOutput Red "  ❌ PostgreSQL 未在预期时间内就绪，请检查容器日志。"
            exit 1
        }
    }
    "mysql" {
        $readyFlag = $false
        Write-Host "  ⏳ MariaDB 初始化中，可能需要 15~60 秒，请耐心等待..."
        Start-Sleep -Seconds 15
        for ($i = 0; $i -lt 60; $i++) {
            $test = docker exec $DB_CONTAINER mysql -h 127.0.0.1 -uroot --password=$MYSQL_ROOT_PASSWORD -e "SELECT 1" 2>&1
            if ($LASTEXITCODE -eq 0) {
                Write-ColorOutput Green "  ✅ MariaDB 已就绪 (尝试 $($i+1) 次)"
                $readyFlag = $true
                break
            } else {
                if (($i % 10) -eq 0) {
                    Write-Host "  ... 第 $($i+1) 次尝试失败，错误：$test"
                }
                Start-Sleep -Seconds 2
            }
        }
        if (-not $readyFlag) {
            Write-ColorOutput Red "  ❌ MariaDB 未在预期时间内就绪，请查看容器日志：docker logs $DB_CONTAINER"
            exit 1
        }
    }
    "sqlserver" {
        Write-Host "  ⏳ SQL Server 启动中，可能需要 20-30 秒..."
        Start-Sleep -Seconds 30
        Write-Host "  ℹ️  尝试创建数据库 $MSSQL_DB_NAME ..."
        $createResult = docker exec $DB_CONTAINER /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -Q "IF DB_ID('$MSSQL_DB_NAME') IS NULL CREATE DATABASE [$MSSQL_DB_NAME]" 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-ColorOutput Yellow "  ⚠️  自动创建数据库失败，请稍后手动创建或确认容器内 sqlcmd 路径"
            Write-Host "  错误信息：$createResult"
        } else {
            Write-ColorOutput Green "  ✅ SQL Server 已就绪"
        }
    }
}

# ---- 7.5 如果是 MySQL 分支，询问是否安装 phpMyAdmin ----
$PMA_INSTALLED = $false
if ($DB_TYPE -eq "mysql") {
    Write-ColorOutput Yellow "[7.5/10] 是否安装 phpMyAdmin 管理界面？"
    Write-Host "  默认不安装，30秒内无输入自动跳过。"
    Write-Host "  [提示] 请输入 Y (安装) 或 N (跳过) 后按回车确认。"
    $pmaInput = Read-HostWithTimeout -Prompt "是否安装 phpMyAdmin? [y/N]: " -TimeoutSeconds 30 -DefaultValue "N"
    Write-Host "您选择了: $pmaInput"

    if ($pmaInput -match "^[yY]$") {
        Write-ColorOutput Yellow "  开始安装 phpMyAdmin..."
        # 清理旧 phpMyAdmin 容器（若存在）
        $oldPma = docker ps -a -q -f "name=^${PMA_CONTAINER}$"
        if ($oldPma) {
            Write-Host "  → 发现旧 phpMyAdmin 容器 (ID: $oldPma)，正在停止并删除..."
            docker stop $oldPma 2>$null
            docker rm $oldPma 2>$null
            Write-ColorOutput Green "  ✅ 旧 phpMyAdmin 容器已删除"
        } else {
            Write-ColorOutput Yellow "  ℹ️  未找到名为 $PMA_CONTAINER 的容器"
        }

        # 启动新 phpMyAdmin 容器
        $pmaArgs = @(
            "run", "--name", $PMA_CONTAINER,
            "--network", $NETWORK,
            "-e", "PMA_HOST=$DB_CONTAINER",
            "-e", "PMA_PORT=$DB_PORT_CONTAINER",
            "-p", "${PMA_PORT_HOST}:${PMA_PORT_CONTAINER}",
            "-d", $PMA_IMAGE
        )
        & docker $pmaArgs

        if ($LASTEXITCODE -eq 0) {
            Write-ColorOutput Green "  ✅ phpMyAdmin 已启动（端口: $PMA_PORT_HOST）"
            $PMA_INSTALLED = $true
        } else {
            Write-ColorOutput Red "  ❌ phpMyAdmin 启动失败，请检查错误信息。"
            # 不退出，继续后续步骤
        }
    } else {
        Write-ColorOutput Green "  ✅ 跳过 phpMyAdmin 安装。"
    }
}

# ---- 8. 清理旧 WebAPI 容器 ----
Write-ColorOutput Yellow "[8/10] 清理旧的 WebAPI 容器..."
$oldApi = docker ps -a -q -f "name=^${API_CONTAINER}$"
if ($oldApi) {
    Write-Host "  → 发现旧容器 (ID: $oldApi)，正在停止并删除..."
    docker stop $oldApi 2>$null
    docker rm $oldApi 2>$null
    Write-ColorOutput Green "  ✅ 旧 WebAPI 容器已删除"
} else {
    Write-ColorOutput Yellow "  ℹ️  未找到名为 $API_CONTAINER 的容器"
}

# ---- 9. 选择镜像源并拉取最新 WebAPI 镜像 ----
Write-ColorOutput Yellow "[9/10] 请选择 WebAPI 镜像源（30秒内无输入默认使用官方源）"
Write-Host "  1) 官方源 (Docker Hub) (默认)"
Write-Host "     $API_IMAGE_OFFICIAL"
Write-Host "  2) 阿里云源"
Write-Host "     $API_IMAGE_ALIYUN"
Write-Host "  3) 腾讯云源"
Write-Host "     $API_IMAGE_TENCENT"
Write-Host "  4) 华为云源"
Write-Host "     $API_IMAGE_HUAWEI"
Write-Host "  [提示] 请输入数字（1-4）后按回车确认。"

$imageSourceChoice = Read-HostWithTimeout -Prompt "请输入选项 [1-4]: " -TimeoutSeconds 30 -DefaultValue "1"
Write-Host "您选择了: $imageSourceChoice"

if ($imageSourceChoice -eq $null -or $imageSourceChoice -eq "") {
    $imageSourceChoice = "1"
    Write-ColorOutput Yellow "  ⏰ 超时未输入，自动使用官方源"
}

$API_IMAGE = ""
switch ($imageSourceChoice) {
    "1" {
        $API_IMAGE = $API_IMAGE_OFFICIAL
        Write-ColorOutput Green "  ✅ 选择镜像源: 官方源"
    }
    "2" {
        $API_IMAGE = $API_IMAGE_ALIYUN
        Write-ColorOutput Green "  ✅ 选择镜像源: 阿里云源"
    }
    "3" {
        $API_IMAGE = $API_IMAGE_TENCENT
        Write-ColorOutput Green "  ✅ 选择镜像源: 腾讯云源"
    }
    "4" {
        $API_IMAGE = $API_IMAGE_HUAWEI
        Write-ColorOutput Green "  ✅ 选择镜像源: 华为云源"
    }
    default {
        Write-ColorOutput Red "❌ 无效选项，使用官方源"
        $API_IMAGE = $API_IMAGE_OFFICIAL
    }
}

# ---- 9. 拉取最新 WebAPI 镜像 ----
Write-ColorOutput Yellow "[9/10] 拉取最新 WebAPI 镜像: $API_IMAGE ..."
docker pull $API_IMAGE
Write-ColorOutput Green "  ✅ WebAPI 镜像拉取完成"
docker image prune -f

# ---- 10. 准备上传目录并启动 WebAPI ----
Write-ColorOutput Yellow "[10/10] 准备宿主机上传目录并启动 WebAPI..."
New-Item -ItemType Directory -Force -Path $UPLOAD_HOST_DIR | Out-Null
Write-ColorOutput Green "  ✅ 目录已创建: $UPLOAD_HOST_DIR"

Write-Host "  ⏳ 生成安全的 JWT 密钥..."
$bytes = New-Object byte[] 32
[System.Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($bytes)
$JWT_SIGN_KEY = [System.Convert]::ToBase64String($bytes)
Write-ColorOutput Green "  ✅ JWT 密钥已生成（长度: $($JWT_SIGN_KEY.Length) 字符）"

# ---- 询问是否启用数据库自动迁移 ----
Write-ColorOutput Yellow "  是否启用数据库自动迁移（AUTO_MIGRATE）？"
Write-Host "  AUTO_MIGRATE=true 会在应用启动时自动执行数据库迁移（EF Core Migrate）。"
Write-Host "  默认启用（true），30秒内无输入自动启用。"
Write-Host "  [提示] 请输入 Y (启用) 或 N (禁用) 后按回车确认。"
$migrateInput = Read-HostWithTimeout -Prompt "启用自动迁移? [Y/n]: " -TimeoutSeconds 30 -DefaultValue "Y"
Write-Host "您选择了: $migrateInput"

$AUTO_MIGRATE = "true"
if ($migrateInput -match "^[nN]$") {
    $AUTO_MIGRATE = "false"
}

if ($AUTO_MIGRATE -eq "true") {
    Write-ColorOutput Green "  ✅ 将启用自动迁移（AUTO_MIGRATE=true）"
} else {
    Write-ColorOutput Yellow "  ⚠️  将禁用自动迁移（AUTO_MIGRATE=false），请确保数据库结构已就绪，否则应用可能启动失败"
}

$dockerUploadPath = $UPLOAD_HOST_DIR -replace '\\', '/'
$apiArgs = @(
    "run", "--name", $API_CONTAINER,
    "--network", $NETWORK,
    "-v", "${dockerUploadPath}:/userData",
    "-p", "${API_HOST_PORT}:${API_CONTAINER_PORT}",
    "-e", "Database__Provider=$DB_PROVIDER",
    "-e", "ConnectionStrings__${DB_PROVIDER}=$DB_CONNECTION_STRING",
    "-e", "AUTO_MIGRATE=$AUTO_MIGRATE",
    "-e", "Authentication__Jwt__Sign=$JWT_SIGN_KEY",
    "-d", $API_IMAGE
)
& docker $apiArgs

if ($LASTEXITCODE -eq 0) {

    # ---------- 询问是否设置开机自启 ----------
    Write-ColorOutput Yellow "[可选] 是否将已安装的容器设置为开机自动启动？"
    Write-Host "  默认不设置（容器不会随 Docker 服务自动启动）。"
    Write-Host "  若选择是，将对数据库、WebAPI 以及可能的 phpMyAdmin 设置 --restart unless-stopped。"
    Write-Host "  [提示] 请输入 Y (设置) 或 N (不设置) 后按回车确认。"
    $restartInput = Read-HostWithTimeout -Prompt "设置开机自启? [y/N]: " -TimeoutSeconds 30 -DefaultValue "N"
    Write-Host "您选择了: $restartInput"

    if ($restartInput -match "^[yY]$") {
        Write-ColorOutput Yellow "  正在设置自动重启策略..."
        # 数据库容器
        docker update --restart unless-stopped $DB_CONTAINER | Out-Null
        Write-ColorOutput Green "  ✅ 已设置 $DB_CONTAINER 自动重启"
        # WebAPI 容器
        docker update --restart unless-stopped $API_CONTAINER | Out-Null
        Write-ColorOutput Green "  ✅ 已设置 $API_CONTAINER 自动重启"
        # 如果安装了 phpMyAdmin，也设置
        if ($PMA_INSTALLED) {
            docker update --restart unless-stopped $PMA_CONTAINER | Out-Null
            Write-ColorOutput Green "  ✅ 已设置 $PMA_CONTAINER 自动重启"
        }
    } else {
        Write-ColorOutput Green "  ✅ 未设置自动重启，容器将保持默认策略。"
    }

    # ---------- 获取宿主机局域网 IP ----------
    $HOST_IP = $null
    try {
        $defaultRoute = Get-NetRoute -DestinationPrefix '0.0.0.0/0' | Sort-Object RouteMetric | Select-Object -First 1
        if ($defaultRoute) {
            $interfaceIndex = $defaultRoute.InterfaceIndex
            $ip = Get-NetIPAddress -AddressFamily IPv4 -InterfaceIndex $interfaceIndex | Where-Object { $_.IPAddress -notlike '127.*' } | Select-Object -First 1
            if ($ip) { $HOST_IP = $ip.IPAddress }
        }
    } catch { }
    if (-not $HOST_IP) {
        $HOST_IP = (Get-NetIPAddress -AddressFamily IPv4 | Where-Object { $_.IPAddress -notlike '127.*' -and $_.IPAddress -notlike '169.254.*' -and $_.InterfaceAlias -notmatch 'Loopback|Docker|vEthernet' } | Select-Object -First 1).IPAddress
    }

    Write-Host "========================================="
    Write-ColorOutput Green "  ✅ 全部部署成功！"
    Write-Host "  ● 网络: $NETWORK"
    Write-Host "  ● 数据库: $DB_TYPE (容器 $DB_CONTAINER，端口 $DB_PORT_HOST，数据目录 $DB_VOLUME_HOST)"
    Write-Host "  ● WebAPI: 容器 $API_CONTAINER"
    Write-Host "      - 镜像源:          $API_IMAGE"
    Write-Host "      - 本机访问:        http://localhost:${API_HOST_PORT}"
    if ($HOST_IP) {
        Write-Host "      - 局域网/对外IP:   http://${HOST_IP}:${API_HOST_PORT}"
    } else {
        Write-Host "      - 未获取到宿主机 IP，请手动检查网络配置"
    }
    Write-Host "  ● 上传目录: $UPLOAD_HOST_DIR（挂载到容器内 /userData）"
    Write-Host "  ● JWT 密钥: 已注入（未显示）"
    Write-Host "  ● 数据库自动迁移(AUTO_MIGRATE): $AUTO_MIGRATE"

    # MySQL 分支的 phpMyAdmin 信息
    if ($DB_TYPE -eq "mysql") {
        if ($PMA_INSTALLED) {
            Write-Host "  ● phpMyAdmin: 容器 $PMA_CONTAINER"
            Write-Host "      - 访问地址:        http://localhost:${PMA_PORT_HOST}"
            if ($HOST_IP) {
                Write-Host "      - 局域网/对外IP:   http://${HOST_IP}:${PMA_PORT_HOST}"
            }
        } else {
            Write-Host "  💡 可选的 phpMyAdmin 管理界面（如需要，请手动执行）："
            Write-Host "     docker run --name $PMA_CONTAINER --network=$NETWORK -e PMA_HOST=$DB_CONTAINER -e PMA_PORT=$DB_PORT_CONTAINER -p ${PMA_PORT_HOST}:${PMA_PORT_CONTAINER} -d $PMA_IMAGE"
        }
    }

    Write-Host "========================================="
    Write-Host "  查看日志:"
    Write-Host "    docker logs -f $DB_CONTAINER"
    Write-Host "    docker logs -f $API_CONTAINER"
    if ($PMA_INSTALLED) {
        Write-Host "    docker logs -f $PMA_CONTAINER"
    }
    Write-Host "  进入容器:"
    Write-Host "    docker exec -it $API_CONTAINER /bin/bash"
} else {
    Write-ColorOutput Red "  ❌ WebAPI 启动失败，请检查错误信息。"
    exit 1
}