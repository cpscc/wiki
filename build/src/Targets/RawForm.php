<?php
namespace Cornerstone\Quarry\Targets;

class RawForm
{
    const name = "raw.x-www-form-urlencoded";

    function convert(array $spec, array $config, $h)
    {
        $config = $config[$spec['version']];
        fwrite($h, "$spec[method] $config[path]$spec[path] HTTP/1.1\n");
        fwrite($h, "Host: $config[endpoint]\n");

        if ($spec['body']) {
            if ($spec['version'] == 'v1') {
                fwrite($h, "Authentication: basic " . base64_encode("$config[user]:$config[pass]") . "\n");
            }
            $body = http_build_query($spec['body']);

            fwrite($h, "Accept: application/x-www-form-urlencoded\n");
            fwrite($h, "Content-Type: application/x-www-form-urlencoded\n");
            fwrite($h, "Content-Length: " . strlen($body) . "\n\n");
            fwrite($h, $body . "\n");
        } else {
            fwrite($h, "Accept: application/x-www-form-urlencoded\n");
        }
    }
}
