<?php
namespace Cornerstone\Quarry;

class Spec
{
    function cli($argc, $argv)
    {
        $script = array_shift($argv);
        $command = array_shift($argv);
        $config = $this->config();

        if ($argc < 2) {
            echo "ERROR: at least 1 argument is required\n";
            exit(1);
        }

        $class = 'Cornerstone\\Quarry\\Spec\\' . ucwords($command);
        if (class_exists($class)) {
            (new $class)->cli($argv, $config);
        } else {
            echo "ERROR: unknown sub-command \"$argv[1]\"\n";
            exit(1);
        }
    }

    function config()
    {
        $config = parse_ini_file('../defaults.ini', true);
        $config['v1']['user'] = getenv('V1_USER') ?: $config['v1']['user'];
        $config['v1']['pass'] = getenv('V1_PASS') ?: $config['v1']['pass'];
        $config['v1']['endpoint'] = getenv('V1_ENDPOINT') ?: $config['v1']['endpoint'];
        $config['v1']['path'] = getenv('V1_PATH') ?: $config['v1']['path'];
        return $config;
    }
}
