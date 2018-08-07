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
        fwrite($h, "Accept: application/x-www-form-urlencoded\n");

        if ($spec['body']) {
            $body = http_build_query($spec['body']);
            fwrite($h, "Content-Type: application/x-www-form-urlencoded\n");
            fwrite($h, "Content-Length: " . strlen($body) . "\n\n");
            fwrite($h, $body . "\n");
        }
    }
}
