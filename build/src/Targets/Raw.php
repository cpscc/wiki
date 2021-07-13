<?php
namespace Cornerstone\Quarry\Targets;

class Raw
{
    function convert(array $spec, array $config, $h)
    {
        $config = $config[$spec['version']];
        fwrite($h, "$spec[method] $config[path]$spec[path] HTTP/1.1\n");
        fwrite($h, "Host: $config[endpoint]\n");

        if ($spec['body']) {
            if ($spec['version'] == 'v1') {
                fwrite($h, "Authentication: basic " .
                    base64_encode("$config[user]:$config[pass]") . "\n");
            }
            switch (self::name) {
            case 'raw.json':
                $body = json_encode($spec['body']);
                break;
            case 'raw.x-www-form-urlencoded':
                $body = http_build_query($spec['body']);
                break;
            case 'raw.csv':
                $body = http_build_query($spec['body']);
                break;
            }

            fwrite($h, "Accept: " . self::ctype . "\n");
            fwrite($h, "Content-Type: " . self::ctype . "\n");
            fwrite($h, "Content-Length: " . strlen($body) . "\n\n");
            fwrite($h, $body . "\n");
        } else {
            fwrite($h, "Accept: " . self::ctype . "\n");
        }
    }
}

