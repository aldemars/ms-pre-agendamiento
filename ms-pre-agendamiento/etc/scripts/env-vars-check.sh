#!/bin/bash

status=0;
# Mandatory Env Variables
if [[ -z "${ConnectionStrings__database}" ]]; then echo "Mandatory env variable \"\$ConnectionStrings__database\" is empty"; status=1; fi

#Optional Env Variables
if [[ -z "${ConnectionStrings__AppConfig}" ]] ; then echo "Optional env variable \"\ConnectionStrings__AppConfig\" is empty"; fi
exit $status